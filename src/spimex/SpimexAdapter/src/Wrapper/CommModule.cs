using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SpimexAdapter.Wrapper
{
    internal class CommModule
    {
        public static Encoding DefaultEncoding { get; } = Encoding.GetEncoding(1251);

        #region Declarations

        [DllImport("ZMQCommClient.dll", EntryPoint = "GetICommModule")]
        private static extern IntPtr GetICommModuleZMQ();

        [DllImport("CProClient.dll", EntryPoint = "GetICommModule")]
        private static extern IntPtr GetICommModuleCPro();

        [StructLayout(LayoutKind.Sequential)]
        struct CommModuleClass
        {
            public IntPtr Start;
            public IntPtr Stop;
            public IntPtr Connect;
            public IntPtr SetQueue;
            public IntPtr Disconnect;
            public IntPtr GetLastError;
        }

        private static class Delegates
        {
            [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
            public delegate bool Start(IntPtr ptr, string iniFile, string commonSectonName);

            [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
            public delegate bool Stop(IntPtr ptr);

            [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
            public delegate IntPtr Connect(IntPtr ptr, IntPtr input, string connSectonName);

            [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
            public delegate void SetQueue(IntPtr ptr, IntPtr connect, IntPtr output);

            [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
            public delegate bool Disconnect(IntPtr ptr, IntPtr connect);

            [UnmanagedFunctionPointer(CallingConvention.ThisCall)]
            public delegate int GetLastError(IntPtr ptr, IntPtr connect, IntPtr descrBuf, int bufLen);
        }

        #endregion

        private readonly IntPtr classPtr;

        #region Delegates

        private readonly Delegates.Start startFunc;
        private readonly Delegates.Stop stopFunc;
        private readonly Delegates.Connect connectFunc;
        private readonly Delegates.SetQueue setQueueFunc;
        private readonly Delegates.Disconnect disconnectFunc;
        private readonly Delegates.GetLastError getLastErrorFunc;

        #endregion

        public CommModule(bool crypto)
        {
            classPtr = crypto ? GetICommModuleCPro() : GetICommModuleZMQ();

            IntPtr vtbl = Marshal.ReadIntPtr(classPtr, 0);
            var funcPtr = (CommModuleClass)Marshal.PtrToStructure(vtbl, typeof(CommModuleClass));

            startFunc = (Delegates.Start)Marshal.GetDelegateForFunctionPointer(funcPtr.Start, typeof(Delegates.Start));
            stopFunc = (Delegates.Stop)Marshal.GetDelegateForFunctionPointer(funcPtr.Stop, typeof(Delegates.Stop));
            connectFunc = (Delegates.Connect)Marshal.GetDelegateForFunctionPointer(funcPtr.Connect, typeof(Delegates.Connect));
            setQueueFunc = (Delegates.SetQueue)Marshal.GetDelegateForFunctionPointer(funcPtr.SetQueue, typeof(Delegates.SetQueue));
            disconnectFunc = (Delegates.Disconnect)Marshal.GetDelegateForFunctionPointer(funcPtr.Disconnect, typeof(Delegates.Disconnect));
            getLastErrorFunc = (Delegates.GetLastError)Marshal.GetDelegateForFunctionPointer(funcPtr.GetLastError, typeof(Delegates.GetLastError));
        }

        public bool Start(string iniFile, string commonSectonName) => startFunc(classPtr, iniFile, commonSectonName);

        public bool Stop() => stopFunc(classPtr);

        public IntPtr Connect(CommQueue input, string connSectonName) => connectFunc(classPtr, input.Queue, connSectonName);

        public void SetQueue(IntPtr connect, CommQueue output) => setQueueFunc(classPtr, connect, output.Queue);

        public void Disconnect(IntPtr connect) => disconnectFunc(classPtr, connect);

        public int GetLastError(IntPtr connect, out string descrBuf)
        {
            const int buffLength = 1000;

            IntPtr buff = Marshal.AllocHGlobal(buffLength);

            var result = getLastErrorFunc(classPtr, connect, buff, buffLength);

            var mBuffer = new byte[buffLength];

            Marshal.Copy(buff, mBuffer, 0, buffLength);
            Marshal.FreeHGlobal(buff);

            var zeroIndex = Array.IndexOf<byte>(mBuffer, 0);
            descrBuf = DefaultEncoding.GetString(mBuffer, 0, zeroIndex);

            return result;
        }

    }
}
