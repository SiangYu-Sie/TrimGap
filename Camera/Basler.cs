using Basler.Pylon;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Camera
{
    public class BaslerCCD
    {

        public Basler.Pylon.Camera BaslerC = null;
        public string Camera_serial = "";
        public int Gain = 0;
        public int CameraWidth, CameraHeight, CameraHeightOffset, CameraWidthOffset;
        public int Image_Width = 0;
        public int Image_Height = 0;
        public IntPtr Image_Ptr = new IntPtr();
        //private Halcon_Method HalconMothed = new Halcon_Method();
        //public HObject _image;
        public byte[] _imageData;
        PixelDataConverter converter = new PixelDataConverter();
        IntPtr LastestFrameAdress;

        //private LogMsg _Log = new LogMsg("Basler");

        public BaslerCCD(string CameraName)  //進來從1開始 設定上讀檔案不用CCD 是0
        {         
            
            Console.WriteLine("Basler init start");
            int no = -1;
            try
            {
                if(CameraName == "")
                    BaslerC = new Basler.Pylon.Camera();
                else
                    BaslerC = new Basler.Pylon.Camera(CameraName);
                BaslerC.CameraOpened += Configuration.SoftwareTrigger;
                BaslerC.Open();
                BaslerC.Parameters[PLCamera.TriggerSelector].SetValue(PLCamera.TriggerSelector.FrameStart);
                BaslerC.Parameters[PLCamera.TriggerSource].SetValue(PLCamera.TriggerSource.Software);
                BaslerC.Parameters[PLCamera.TriggerMode].SetValue(PLCamera.TriggerMode.On);
                BaslerC.Parameters[PLCameraInstance.MaxNumBuffer].SetValue(10);
                StartStream();
                Console.WriteLine("Basler init compelete");
                //Console.WriteLine("Advance Stream");
            }
            catch (Exception ex)
            {
                //ArenaNET.Arena.();
                throw ex;
            }
            finally
            {
                
            }

            Console.WriteLine("Basler init end");
        }

        public void setParameter(long gain, long width, long height, long widthoffset, long heightoffset)
        {
            BaslerC.Parameters[PLGigECamera.GainRaw].SetValue(gain);
            BaslerC.Parameters[PLGigECamera.Width].SetValue(width);
            BaslerC.Parameters[PLGigECamera.Height].SetValue(height);
            BaslerC.Parameters[PLGigECamera.OffsetX].SetValue(widthoffset);
            BaslerC.Parameters[PLGigECamera.OffsetY].SetValue(heightoffset);
        }

        public void closeCamera()
        {
            if (BaslerC.StreamGrabber.IsGrabbing)
                BaslerC.StreamGrabber.Stop();
            BaslerC.Close();

        }

        private void StartStream()
        {

            if (!BaslerC.StreamGrabber.IsGrabbing)
                BaslerC.StreamGrabber.Start();

        }

        private void StopStream()
        {
            if (BaslerC.StreamGrabber.IsGrabbing)
                BaslerC.StreamGrabber.Stop();
        }

        public void GrabImage()
        {
            try
            {
                StartStream();
                BaslerC.ExecuteSoftwareTrigger();
                //camera.StreamGrabber.Start(1, GrabStrategy.LatestImages, GrabLoop.ProvidedByUser);
                IGrabResult grabResult = BaslerC.StreamGrabber.RetrieveResult(500, TimeoutHandling.ThrowException);
                BaslerC.StreamGrabber.Stop();
                //IGrabResult grabResult = camera.StreamGrabber.RetrieveResult(5000, TimeoutHandling.ThrowException);
                if (grabResult.IsValid)
                {
                    if (grabResult.PixelTypeValue == PixelType.Mono8)
                    {

                        LastestFrameAdress = Marshal.AllocHGlobal((Int32)grabResult.PayloadSize);
                        converter.OutputPixelFormat = PixelType.Mono8;
                        converter.Convert(LastestFrameAdress, grabResult.PayloadSize, grabResult);
                        _imageData = grabResult.PixelData as byte[];
                        Image_Width = grabResult.Width;
                        Image_Height = grabResult.Height;
                        Marshal.FreeHGlobal(LastestFrameAdress);
                    }
                    else if (grabResult.PixelTypeValue == PixelType.RGB8packed)
                    {

                        LastestFrameAdress = Marshal.AllocHGlobal((Int32)grabResult.PayloadSize);
                        converter.OutputPixelFormat = PixelType.RGB8packed;
                        converter.Convert(LastestFrameAdress, grabResult.PayloadSize, grabResult);
                        _imageData = grabResult.PixelData as byte[];
                        Image_Width = grabResult.Width;
                        Image_Height = grabResult.Height;
                        Marshal.FreeHGlobal(LastestFrameAdress);
                    }
                }
                grabResult.Dispose();

            }
            catch (Exception ex)
            {
                BaslerC.StreamGrabber.Stop();
                BaslerC.Close();
            }
        }

        public void setExposure(bool auto, double value)
        {
            if (BaslerC.IsOpen)
                BaslerC.Parameters[PLCamera.ExposureTimeAbs].SetValue(value);
        }
    }
}
