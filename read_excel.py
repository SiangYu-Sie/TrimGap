import sys
import xml.etree.ElementTree as ET
import zipfile

def read_xlsx(filename):
    with zipfile.ZipFile(filename, 'r') as z:
        # Get shared strings
        shared_strings = []
        try:
            with z.open('xl/sharedStrings.xml') as f:
                tree = ET.parse(f)
                root = tree.getroot()
                # Default namespace mapping, typically '{http://schemas.openxmlformats.org/spreadsheetml/2006/main}'
                ns = {'main': 'http://schemas.openxmlformats.org/spreadsheetml/2006/main'}
                for si in root.findall('main:si', ns):
                    t = si.find('main:t', ns)
                    if t is not None:
                        shared_strings.append(t.text)
                    else:
                        shared_strings.append(''.join(x.text for x in si.findall('.//main:t', ns) if x.text is not None))
        except KeyError:
            pass
        
        # Get sheet info
        with z.open('xl/workbook.xml') as f:
            tree = ET.parse(f)
            root = tree.getroot()
            ns = {'main': 'http://schemas.openxmlformats.org/spreadsheetml/2006/main', 'r': 'http://schemas.openxmlformats.org/officeDocument/2006/relationships'}
            sheets = root.find('main:sheets', ns).findall('main:sheet', ns)
            
        for idx, sheet in enumerate(sheets):
            sheet_name = sheet.get('name')
            print(f'--- Sheet: {sheet_name} ---')
            with z.open(f'xl/worksheets/sheet{idx+1}.xml') as f:
                tree = ET.parse(f)
                root = tree.getroot()
                
                for row in root.findall('.//main:row', ns):
                    row_data = []
                    for c in row.findall('main:c', ns):
                        t = c.get('t')
                        v = c.find('main:v', ns)
                        if v is not None:
                            val = v.text
                            if t == 's':  # Shared string
                                val = shared_strings[int(val)]
                            row_data.append(str(val))
                        else:
                            row_data.append('')
                    if any(row_data):
                        print('|'.join(row_data))

if __name__ == '__main__':
    read_xlsx(sys.argv[1])
