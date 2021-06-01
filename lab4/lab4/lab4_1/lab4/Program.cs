using System;
using System.Runtime.InteropServices;
using System.Text;


namespace lab4
{
    class Processor
    {
        public short ProcessorArchitecture { get; set; }
        public uint PageSize { get; set; }
        public IntPtr MinimumApplicationAddress { get; set; }
        public IntPtr MaximumApplicationAddress { get; set; }
        public IntPtr ActiveProcessorMask { get; set; }
        public uint NumberOfProcessors { get; set; }
        public uint ProcessorType { get; set; }
        public uint AllocationGranularity { get; set; }
        public ushort ProcessorLevel { get; set; }
        public ushort ProcessorRevision { get; set; }
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    class Memory
    {
        public uint Length { get; set; }
        public uint MemoryLoad { get; set; }
        public ulong TotalPhys { get; set; }
        public ulong AvailPhys { get; set; }
        public ulong TotalPageFile { get; set; }
        public ulong AvailPageFile { get; set; }
        public ulong TotalVirtual { get; set; }
        public ulong AvailVirtual { get; set; }
        public ulong AvailExtendedVirtual { get; set; }

        public Memory()
        {
            Length = (uint)Marshal.SizeOf(typeof(Memory));
        }
    }
    class SystemInfo
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool GlobalMemoryStatusEx(Memory info);

        [DllImport("kernel32.dll", SetLastError = false)]
        public static extern void GetSystemInfo(Processor info);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool GetDiskFreeSpaceEx(string lpDirectoryName, out ulong lpFreeBytesAvailable, out ulong lpTotalNumberOfBytes, out ulong lpTotalNumberOfFreeBytes);

        public static string GetMemoryInfo()
        {
            Memory info = new Memory();
            GlobalMemoryStatusEx(info);

            StringBuilder str = new StringBuilder();
            str.Append("Memory architecture: " + info.Length.ToString() + " bit\n");
            str.Append("Memory Load: " + info.MemoryLoad.ToString() + "%" + "\n");
            str.Append("Total Memory: " + (info.TotalPhys / Math.Pow(2, 30)).ToString() + " " + "GB" + "\n");
            str.Append("Available Memory: " + (info.AvailPhys / Math.Pow(2, 30)).ToString() + " " + "GB" + "\n");
            return str.ToString();
        }

        public static string GetProcessorInfo()
        {
            Processor info = new Processor();
            GetSystemInfo(info);

            StringBuilder str = new StringBuilder("Proccesor architecture: ");
            switch (info.ProcessorArchitecture)
            {
                case 0:
                    str.Append("x64\n");
                    break;
                case 9:
                    str.Append("x86\n");
                    break;
                default:
                    str.Append("others\n");
                    break;
            }
            str.Append("Count of Processors: " + (info.NumberOfProcessors + 1) + "\n");
            return str.ToString();
        }

        public static string GetDiscInfo(string discName)
        {

            ulong FreeBytesAvailable;
            ulong TotalNumberOfBytes;
            ulong TotalNumberOfFreeBytes;

            bool success = GetDiskFreeSpaceEx(discName, out FreeBytesAvailable, out TotalNumberOfBytes, out TotalNumberOfFreeBytes);

            StringBuilder str = new StringBuilder();
            str.Append($"Free Space on disc {discName} {(FreeBytesAvailable / Math.Pow(2, 30)).ToString()}" + " GB\n");
            str.Append($"Total Space on disc {discName} {(TotalNumberOfBytes / Math.Pow(2, 30)).ToString()}" + " GB\n");
            return str.ToString();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(SystemInfo.GetMemoryInfo());
            Console.WriteLine(SystemInfo.GetProcessorInfo());
            Console.WriteLine(SystemInfo.GetDiscInfo("C:\\"));
            Console.WriteLine(SystemInfo.GetDiscInfo("D:\\"));
            Console.ReadKey();
        }
    }
}
