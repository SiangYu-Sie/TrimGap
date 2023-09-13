using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Camera
{
    public class CameraBasic
    {
        private bool init_done = false;

        private int CameraUsingBrand = 0;  //CCD 使用哪家 預設0:Lucid, 1:FAI, 需要增加再改
        //private Lucid Lucid_Camera = null;
        //private FLIR FLIR_Camera = null;
        private BaslerCCD Basler_Camera = null;
        //private DALSA DALSA_Camera = null;
        //private FAI FAI_Camera = null;
        //private LogMsg _Log = new LogMsg("Camera");
        //private Halcon_Method HalconMethod = new Halcon_Method();
        //public HObject image = null;
        public byte[] image;
        public int Width, Height;
        private bool Mirror_Row, Mirror_Col;
        private string CCDName = "";

        public void init(int Brand, string CameraName, bool mirror_r, bool mirror_c)
        {
            try
            {
                CCDName = CameraName;
                CameraUsingBrand = Brand;
                Width = 0;
                Height = 0;
                switch (CameraUsingBrand)
                {
                    case 0:
                        //Lucid_Camera = new Lucid(CameraName);
                        break;
                    case 1:
                        //FLIR_Camera = new FLIR(CameraName);
                        break;
                    case 2:
                        Basler_Camera = new BaslerCCD(CameraName);
                        break;
                    default:
                        init_done = false;
                        //_Log.WriteLog("CCD init Error, Config.ini");
                        throw new InvalidOperationException("CCD init Error, 請檢查Config.ini");
                }
                Mirror_Row = mirror_r;
                Mirror_Col = mirror_c;

                init_done = true;

                Console.WriteLine("測試相機開始");
                //for (int i = 0; i < 10; ++i)
                //{
                    GetImage();
                //    Thread.Sleep(10);
                //}
            }
            catch(Exception ex)
            {
                init_done = false;
                Close();
                //_Log.WriteLog(ex.Message);
                throw ex;
            }
        }

        public void GetImage()
        {
            if (!init_done)
            {
                throw new InvalidOperationException("尚未成功初始化");
            }
            try
            {
                switch (CameraUsingBrand)
                {
                    case 0:
                        //Lucid_Camera.ConfigureTriggerAndAcquireImage();
                        //HOperatorSet.CopyImage(Lucid_Camera._image, out image);
                        /*
                        if (Mirror_Row)
                            image = HalconMethod.MirrorImage_row(Lucid_Camera._image);
                        if (Mirror_Col)
                            image = HalconMethod.MirrorImage_col(Lucid_Camera._image);
                        */
                        //if (!Mirror_Row && !Mirror_Col)
                        //    HOperatorSet.CopyImage(Lucid_Camera._image, out image);
                        break;
                    case 1:
                        //FLIR_Camera.GrabImage();
                        /*
                        if (Mirror_Row)
                            image = HalconMethod.MirrorImage_row(FLIR_Camera._image);
                        if (Mirror_Col)
                            image = HalconMethod.MirrorImage_col(FLIR_Camera._image);
                        */
                        //if (!Mirror_Row && !Mirror_Col)
                        //    HOperatorSet.CopyImage(FLIR_Camera._image, out image);
                        break;
                    case 2:
                        Basler_Camera.GrabImage();
                        /*
                        if (Mirror_Row)
                            image = HalconMethod.MirrorImage_row(Basler_Camera._image);
                        if (Mirror_Col)
                            image = HalconMethod.MirrorImage_col(Basler_Camera._image);
                        */
                        //if (!Mirror_Row && !Mirror_Col)
                        if (image == null) image = new byte[Basler_Camera.Image_Width * Basler_Camera.Image_Height];
                        Array.Copy(Basler_Camera._imageData, image, Basler_Camera.Image_Width* Basler_Camera.Image_Height);
                        Width = Basler_Camera.Image_Width;
                        Height = Basler_Camera.Image_Height;
                        break;
                    default:
                        //_Log.WriteLog("CCD init Error, Config.ini");
                        throw new InvalidOperationException("CCD init Error, 請檢查Config.ini");
                }
            }
            catch (Exception ex)
            {
                //_Log.WriteLog(CCDName + ": 取像失敗");
                //_Log.WriteLog(ex.Message);
                //Reconnect();
                throw new InvalidOperationException(CCDName + ": 取像失敗");
            }
        }
        public void Close()
        {
            try
            {
                switch (CameraUsingBrand)
                {
                    case 0:
                        /*if (Lucid_Camera != null)
                        {
                            Lucid_Camera.closeCamera();
                            Lucid_Camera = null;
                        }*/
                        break;
                    case 1:
                        /*if (FLIR_Camera != null)
                        {
                            FLIR_Camera.closeCamera();
                            FLIR_Camera = null;
                        }*/
                        break;
                    case 2:
                        if(Basler_Camera != null)
                        {
                            Basler_Camera.closeCamera();
                            Basler_Camera = null;
                        }
                        break;
                    default:
                        init_done = false;
                        //_Log.WriteLog("CCD init Error, Config.ini");
                        throw new InvalidOperationException("CCD init Error, 請檢查Config.ini");
                }
                init_done = false;
            }
            catch (Exception ex)
            {
                init_done = false;
                //_Log.WriteLog(ex.Message);
            }
        }

        public void Reconnect()
        {
            //_Log.WriteLog(CCDName + "重啟中");
            Close();
            init(CameraUsingBrand, CCDName, Mirror_Row, Mirror_Col);
            //_Log.WriteLog(CCDName + "重啟完成");
        }

        public void Exposure(bool auto, double value)
        {
            //if (CameraUsingBrand == 0 && Lucid_Camera == null) return;
            //if (CameraUsingBrand == 1 && FLIR_Camera == null) return;
            if (CameraUsingBrand == 2 && Basler_Camera == null) return;

            switch (CameraUsingBrand)
            {
                case 0:
                    //Lucid_Camera.setExposure(auto, value);
                    break;
                case 2:
                    Basler_Camera.setExposure(auto, value);
                    break;
            }
        }

        public void setBaslerParameter(long gain, long width, long height, long widthoffset, long heightoffset)
        {
            if (CameraUsingBrand == 2)
                Basler_Camera.setParameter(gain, width, height, widthoffset, heightoffset);
        }
    }
}
