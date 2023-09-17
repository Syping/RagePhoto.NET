using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RagePhoto.NET
{
    internal class RagePhoto
    {
        private readonly IntPtr Instance;
        public readonly String Version;

        [DllImport("libragephoto.dll")]
        private static extern IntPtr ragephoto_open();

        [DllImport("libragephoto.dll")]
        private static extern IntPtr ragephoto_version();

        [DllImport("libragephoto.dll")]
        private static extern void ragephoto_close(IntPtr instance);

        [DllImport("libragephoto.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ragephoto_load(IntPtr instance, IntPtr data, UIntPtr size);

        [DllImport("libragephoto.dll")]
        private static extern Int32 ragephoto_error(IntPtr instance);

        [DllImport("libragephoto.dll")]
        private static extern IntPtr ragephoto_getphotojpeg(IntPtr instance);

        [DllImport("libragephoto.dll")]
        private static extern UInt32 ragephoto_getphotosize(IntPtr instance);

        [DllImport("libragephoto.dll")]
        private static extern IntPtr ragephoto_getphototitle(IntPtr instance);

        public RagePhoto()
        {
            Instance = ragephoto_open();
            IntPtr versionPtr = ragephoto_version();
            String? versionStr = Marshal.PtrToStringUTF8(versionPtr);
            if (versionStr == null)
                Version = "";
            else
                Version = versionStr;
        }

        ~RagePhoto()
        {
            ragephoto_close(Instance);
        }

        public bool LoadFile(String filePath)
        {
            if (!File.Exists(filePath))
                return false;

            Byte[] data = File.ReadAllBytes(filePath);
            UIntPtr size = (UIntPtr)data.LongLength;
            GCHandle dataHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
            IntPtr dataPtr = dataHandle.AddrOfPinnedObject();

            bool isLoaded = ragephoto_load(Instance, dataPtr, size);

            dataHandle.Free();

            return isLoaded;
        }

        public Int32 GetError()
        {
            return ragephoto_error(Instance);
        }

        public Image? GetJPEG()
        {
            UInt32 size = ragephoto_getphotosize(Instance);
            if (size == 0)
                return null;

            IntPtr jpegPtr = ragephoto_getphotojpeg(Instance);
            Byte[] jpeg = new Byte[size];
            Marshal.Copy(jpegPtr, jpeg, 0, (int)size);
            MemoryStream jpegStream = new(jpeg, 0, (int)size);
            return Image.FromStream(jpegStream);
        }

        public String GetTitle()
        {
            IntPtr titlePtr = ragephoto_getphototitle(Instance);
            String? titleStr = Marshal.PtrToStringUTF8(titlePtr);
            if (titleStr == null)
                return "";
            else
                return titleStr;
        }
    }
}
