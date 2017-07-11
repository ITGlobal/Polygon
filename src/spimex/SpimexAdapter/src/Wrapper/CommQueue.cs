using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace SpimexAdapter.Wrapper
{
    class CommQueue
    {
        #region Declarations

        [DllImport("CommQueueImpl.dll", EntryPoint = "GetICommQueue")]
        private static extern IntPtr GetICommQueue();


        [StructLayout(LayoutKind.Sequential)]
        struct ICommQueue
        {
            public IntPtr Put;
            public IntPtr Get;
            public IntPtr Stop;
            public IntPtr SetAlive;
            public IntPtr Alloc;
            public IntPtr Free;
            public IntPtr GetLastError;
            public IntPtr GetCount;
        }

        private static class Delegates
        {
            [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
            public delegate bool Put(IntPtr ptr, IntPtr connect, IntPtr buffer, int length, int error);

            [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
            public delegate bool Get(IntPtr ptr, out IntPtr connect, out IntPtr buffer, out int length, out int error, long timeout);

            [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
            public delegate bool Stop(IntPtr ptr);

            [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
            public delegate IntPtr Alloc(IntPtr ptr, int length);

            [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
            public delegate bool Free(IntPtr ptr, IntPtr buffer);

            [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
            public delegate int GetLastError(IntPtr ptr, IntPtr connect, IntPtr descrBuf, int bufLen);
        }

        #endregion

        private readonly IntPtr classPtr;

        private readonly Delegates.Put putFunc;
        private readonly Delegates.Get getFunc;
        private readonly Delegates.Stop stopFunc;

        private readonly Delegates.Alloc allocFunc;
        private readonly Delegates.Free freeFunc;
        private readonly Delegates.GetLastError getLastErrorFunc;

        public CommQueue()
        {
            classPtr = GetICommQueue();

            IntPtr vtbl = Marshal.ReadIntPtr(classPtr, 0);
            var funcPtr = (ICommQueue)Marshal.PtrToStructure(vtbl, typeof(ICommQueue));

            putFunc = (Delegates.Put)Marshal.GetDelegateForFunctionPointer(funcPtr.Put, typeof(Delegates.Put));
            getFunc = (Delegates.Get)Marshal.GetDelegateForFunctionPointer(funcPtr.Get, typeof(Delegates.Get));
            stopFunc = (Delegates.Stop)Marshal.GetDelegateForFunctionPointer(funcPtr.Stop, typeof(Delegates.Stop));

            allocFunc = (Delegates.Alloc)Marshal.GetDelegateForFunctionPointer(funcPtr.Alloc, typeof(Delegates.Alloc));
            freeFunc = (Delegates.Free)Marshal.GetDelegateForFunctionPointer(funcPtr.Free, typeof(Delegates.Free));
            getLastErrorFunc = (Delegates.GetLastError)Marshal.GetDelegateForFunctionPointer(funcPtr.GetLastError, typeof(Delegates.GetLastError));
        }

        public IntPtr Queue => classPtr;

        public bool Put(IntPtr connect, byte[] buffer, int error)
        {
            var length = buffer.Length;

            //IntPtr tmpBuff = Marshal.AllocHGlobal(length);
            IntPtr tmpBuff = Alloc(length);
            Marshal.Copy(buffer, 0, tmpBuff, length);

            var result = putFunc(classPtr, connect, tmpBuff, length, error);

            if (!result)
            {
                Free(tmpBuff);
            }

            return result;
        }

        public bool Get(out IntPtr connect, out byte[] buffer, out int error, long timeout)
        {
            IntPtr rBuffer;
            int rLength;

            var result = getFunc(classPtr, out connect, out rBuffer, out rLength, out error, timeout);

            result &= rLength > 0;

            buffer = null;
            if (result)
            {
                buffer = new byte[rLength];
                Marshal.Copy(rBuffer, buffer, 0, rLength);
                Free(rBuffer);
            }

            return result;
        }

        public bool Stop() => stopFunc(classPtr);

        private IntPtr Alloc(int length) => allocFunc(classPtr, length);

        private bool Free(IntPtr buffer) => freeFunc(classPtr, buffer);

        public int GetLastError(IntPtr connect, out string descrBuf)
        {
            const int buffLength = 1000;

            IntPtr buff = Marshal.AllocHGlobal(buffLength);

            var result = getLastErrorFunc(classPtr, connect, buff, buffLength);

            var mBuffer = new byte[buffLength];

            Marshal.Copy(buff, mBuffer, 0, buffLength);
            Marshal.FreeHGlobal(buff);

            var zeroIndex = Array.IndexOf<byte>(mBuffer, 0);
            descrBuf = Encoding.ASCII.GetString(mBuffer, 0, zeroIndex);

            return result;
        }
    }
}
