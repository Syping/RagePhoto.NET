using System.Runtime.InteropServices;

namespace RagePhoto.NET
{
    internal class RagePhoto
    {
        private readonly IntPtr Instance;
        public readonly String Version;
        public enum DefaultSize : UInt32
        {
            DEFAULT_GTA5_PHOTOBUFFER = 524288U,
            DEFAULT_RDR2_PHOTOBUFFER = 1048576U,
            DEFAULT_DESCBUFFER = 256U,
            DEFAULT_JSONBUFFER = 3072U,
            DEFAULT_TITLBUFFER = 256U,
            GTA5_HEADERSIZE = 264U,
            RDR2_HEADERSIZE = 272U,
        }
        public enum Error : Int32
        {
            DescBufferTight = 39,
            DescMallocError = 31,
            DescReadError = 32,
            HeaderBufferTight = 35,
            HeaderMallocError = 4,
            IncompatibleFormat = 2,
            IncompleteChecksum = 7,
            IncompleteDescBuffer = 30,
            IncompleteDescMarker = 28,
            IncompleteDescOffset = 11,
            IncompleteEOF = 8,
            IncompleteHeader = 3,
            IncompleteJendMarker = 33,
            IncompleteJpegMarker = 12,
            IncompleteJsonBuffer = 20,
            IncompleteJsonMarker = 18,
            IncompleteJsonOffset = 9,
            IncompletePhotoBuffer = 14,
            IncompletePhotoSize = 15,
            IncompleteTitleBuffer = 25,
            IncompleteTitleMarker = 23,
            IncompleteTitleOffset = 10,
            IncorrectDescMarker = 29,
            IncorrectJendMarker = 34,
            IncorrectJpegMarker = 13,
            IncorrectJsonMarker = 19,
            IncorrectTitleMarker = 24,
            JsonBufferTight = 37,
            JsonMallocError = 21,
            JsonReadError = 22,
            NoError = 255,
            NoFormatIdentifier = 1,
            PhotoBufferTight = 36,
            PhotoMallocError = 16,
            PhotoReadError = 17,
            TitleBufferTight = 38,
            TitleMallocError = 26,
            TitleReadError = 27,
            UnicodeInitError = 5,
            UnicodeHeaderError = 6,
            Uninitialised = 0,
        }
        public enum PhotoFormat : UInt32
        {
            GTA5 = 0x01000000U,
            RDR2 = 0x04000000U,
        }

        [DllImport("libragephoto.dll")]
        private static extern IntPtr ragephoto_open();
        [DllImport("libragephoto.dll")]
        private static extern void ragephoto_clear(IntPtr instance);
        [DllImport("libragephoto.dll")]
        private static extern void ragephoto_close(IntPtr instance);
        [DllImport("libragephoto.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ragephoto_load(IntPtr instance, IntPtr data, UIntPtr size);
        [DllImport("libragephoto.dll")]
        private static extern Int32 ragephoto_error(IntPtr instance);
        [DllImport("libragephoto.dll")]
        private static extern IntPtr ragephoto_getphotodesc(IntPtr instance);
        [DllImport("libragephoto.dll")]
        private static extern UInt32 ragephoto_getphotoformat(IntPtr instance);
        [DllImport("libragephoto.dll")]
        private static extern IntPtr ragephoto_getphotojpeg(IntPtr instance);
        [DllImport("libragephoto.dll")]
        private static extern IntPtr ragephoto_getphotojson(IntPtr instance);
        [DllImport("libragephoto.dll")]
        private static extern UInt32 ragephoto_getphotosize(IntPtr instance);
        [DllImport("libragephoto.dll")]
        private static extern IntPtr ragephoto_getphototitle(IntPtr instance);
        [DllImport("libragephoto.dll")]
        private static extern UIntPtr ragephoto_getsavesize(IntPtr instance);
        [DllImport("libragephoto.dll")]
        private static extern bool ragephoto_save(IntPtr instance, [Out] Byte[] data);
        [DllImport("libragephoto.dll")]
        private static extern void ragephoto_setphotodesc(IntPtr instance, [MarshalAs(UnmanagedType.LPUTF8Str)] String description, UInt32 bufferSize);
        [DllImport("libragephoto.dll")]
        private static extern void ragephoto_setphotoformat(IntPtr instance, UInt32 photoFormat);
        [DllImport("libragephoto.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ragephoto_setphotojpeg(IntPtr instance, IntPtr jpeg, UInt32 size, UInt32 bufferSize);
        [DllImport("libragephoto.dll")]
        private static extern void ragephoto_setphotojson(IntPtr instance, [MarshalAs(UnmanagedType.LPUTF8Str)] String json, UInt32 bufferSize);
        [DllImport("libragephoto.dll")]
        private static extern void ragephoto_setphototitle(IntPtr instance, [MarshalAs(UnmanagedType.LPUTF8Str)] String title, UInt32 bufferSize);
        [DllImport("libragephoto.dll")]
        private static extern IntPtr ragephoto_version();

        public RagePhoto()
        {
            Instance = ragephoto_open();
            IntPtr versionPtr = ragephoto_version();
            String? versionStr = Marshal.PtrToStringUTF8(versionPtr);
            if (versionStr == null)
                Version = String.Empty;
            else
                Version = versionStr;
        }

        ~RagePhoto()
        {
            ragephoto_close(Instance);
        }

        public void Clear()
        {
            ragephoto_clear(Instance);
        }

        public bool Load(Byte[] data)
        {
            UIntPtr size = (UIntPtr)data.LongLength;
            GCHandle dataHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
            IntPtr dataPtr = dataHandle.AddrOfPinnedObject();
            bool isLoaded = ragephoto_load(Instance, dataPtr, size);
            dataHandle.Free();
            return isLoaded;
        }

        public bool LoadFile(String filePath)
        {
            if (!File.Exists(filePath))
                return false;

            Byte[] data = File.ReadAllBytes(filePath);
            return Load(data);
        }

        public String Description
        {
            get
            {
                IntPtr descPtr = ragephoto_getphotodesc(Instance);
                String? descStr = Marshal.PtrToStringUTF8(descPtr);
                if (descStr == null)
                    return String.Empty;
                return descStr;
            }
            set => ragephoto_setphotodesc(Instance, value, (UInt32)DefaultSize.DEFAULT_DESCBUFFER);
        }

        public UInt32 Format
        {
            get => ragephoto_getphotoformat(Instance);
            set => ragephoto_setphotoformat(Instance, value);
        }

        public Int32 GetError()
        {
            return ragephoto_error(Instance);
        }

        public Byte[] JPEG
        {
            get
            {
                UInt32 size = ragephoto_getphotosize(Instance);
                if (size == 0)
                    return Array.Empty<byte>();

                IntPtr jpegPtr = ragephoto_getphotojpeg(Instance);
                Byte[] jpeg = new Byte[size];
                Marshal.Copy(jpegPtr, jpeg, 0, (int)size);
                return jpeg;
            }
            set
            {
                UInt32 bufferSize;
                UInt32 size = (UInt32)value.Length;
                switch (Format)
                {
                    case (UInt32)PhotoFormat.GTA5:
                        if (size > (UInt32)DefaultSize.DEFAULT_GTA5_PHOTOBUFFER)
                            bufferSize = size;
                        else
                            bufferSize = (UInt32)DefaultSize.DEFAULT_GTA5_PHOTOBUFFER;
                        break;
                    case (UInt32)PhotoFormat.RDR2:
                        if (size > (UInt32)DefaultSize.DEFAULT_RDR2_PHOTOBUFFER)
                            bufferSize = size;
                        else
                            bufferSize = (UInt32)DefaultSize.DEFAULT_RDR2_PHOTOBUFFER;
                        break;
                    default:
                        bufferSize = size;
                        break;
                }

                GCHandle jpegHandle = GCHandle.Alloc(value, GCHandleType.Pinned);
                IntPtr jpegPtr = jpegHandle.AddrOfPinnedObject();
                ragephoto_setphotojpeg(Instance, jpegPtr, size, bufferSize);
                jpegHandle.Free();
            }
        }

        public Image GetJPEGImage()
        {
            MemoryStream jpegStream = new(JPEG);
            return Image.FromStream(jpegStream);
        }

        public UInt32 GetJPEGSize()
        {
            return ragephoto_getphotosize(Instance);
        }

        public String JSON
        {
            get
            {
                IntPtr jsonPtr = ragephoto_getphotojson(Instance);
                String? jsonStr = Marshal.PtrToStringUTF8(jsonPtr);
                if (jsonStr == null)
                    return String.Empty;
                return jsonStr;
            }
            set => ragephoto_setphotojson(Instance, value, (UInt32)DefaultSize.DEFAULT_JSONBUFFER);
        }

        public String Title
        {
            get
            {
                IntPtr titlePtr = ragephoto_getphototitle(Instance);
                String? titleStr = Marshal.PtrToStringUTF8(titlePtr);
                if (titleStr == null)
                    return String.Empty;
                return titleStr;
            }
            set => ragephoto_setphototitle(Instance, value, (UInt32)DefaultSize.DEFAULT_TITLBUFFER);
        }

        public Byte[] Save()
        {
            UIntPtr saveSize = ragephoto_getsavesize(Instance);

            Byte[] jpeg;
            if (Environment.Is64BitProcess)
                jpeg = new Byte[(UInt64)saveSize];
            else
                jpeg = new Byte[(UInt32)saveSize];

            bool isSaved = ragephoto_save(Instance, jpeg);
            if (!isSaved)
                return Array.Empty<Byte>();
            return jpeg;
        }
    }
}
