//Copyright (c) 2020 KEYENCE CORPORATION. All rights reserved.

using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LJXA_ImageAcquisitionSample
{
    public static class CsvConverter
    {
        #region const
        private const int CORRECT_VALUE = 32768;
        private const double INVALID_VALUE = -999.9999;
        #endregion

        #region Method
        public static void Save(string savePath, List<ushort> heightImage, int lines, int width, float zPitchUm)
        {
            // Save the profile
            using (FileStream stream = new FileStream(savePath, FileMode.Create))
            using (StreamWriter streamWriter = new StreamWriter(stream, new UTF8Encoding(false)))
            {
                var sb = new StringBuilder();

                for (int i = 0; i < lines; i++)
                {
                    for (int j = 0; j < width; j++)
                    {
                        int index = (i * width) + j;
                        double value = heightImage[index] == 0 ? INVALID_VALUE : (heightImage[index] - CORRECT_VALUE) * zPitchUm / 1000;
                        sb.Append($"{value:F4}");

                        if (j != (width - 1))
                        {
                            sb.Append(",");
                        }
                    }
                    streamWriter.WriteLine(sb);
                    sb.Clear();
                }
                if (0 < sb.Length) streamWriter.WriteLine(sb);
            }
        }
        public static void Save1D(string savePath, List<ushort> heightImage, int lines, int width, float zPitchUm)
        {
            // Save the profile
            using (FileStream stream = new FileStream(savePath, FileMode.Create))
            using (StreamWriter streamWriter = new StreamWriter(stream, new UTF8Encoding(false)))
            {
                var sb = new StringBuilder();

                for (int i = 0; i < width; i++)
                {
                    int index = i;
                    double value = heightImage[index] == 0 ? INVALID_VALUE : (heightImage[index] - CORRECT_VALUE) * zPitchUm / 1000;
                    sb.Append($"{value:F4}");
                    sb.Append(",");
                }
                if (0 < sb.Length) streamWriter.WriteLine(sb);
            }
        }

        #endregion
    }
}
