﻿namespace Nikse.SubtitleEdit.Logic
{
    using System;
    using System.IO;
    using System.Text;

    public class TarHeader
    {
        public const int HeaderSize = 512;

        public string FileName { get; set; }

        public long FileSizeInBytes { get; set; }

        public long FilePosition { get; set; }

        private readonly Stream stream;

        public TarHeader(Stream stream)
        {
            this.stream = stream;
            var buffer = new byte[HeaderSize];
            stream.Read(buffer, 0, HeaderSize);
            FilePosition = stream.Position;

            FileName = Encoding.ASCII.GetString(buffer, 0, 100).Replace("\0", string.Empty);

            string sizeInBytes = Encoding.ASCII.GetString(buffer, 124, 11);
            if (!string.IsNullOrEmpty(FileName) && Utilities.IsInteger(sizeInBytes))
            {
                FileSizeInBytes = Convert.ToInt64(sizeInBytes.Trim(), 8);
            }
        }

        public void WriteData(string fileName)
        {
            var buffer = new byte[FileSizeInBytes];
            stream.Position = FilePosition;
            stream.Read(buffer, 0, buffer.Length);
            File.WriteAllBytes(fileName, buffer);
        }
    }
}