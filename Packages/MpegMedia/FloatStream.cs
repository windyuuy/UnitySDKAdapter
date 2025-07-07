using System;
using System.Collections.Generic;
using UnityEngine;

namespace MpegMedia.Audio
{
    public class FloatStream : IDisposable
    {
        public const int PageSize = 1024 * 16;
        protected List<float[]> Buffers;

        protected static Queue<float[]> BufferPool = new();

        public static float[] Alloc()
        {
            if (BufferPool.Count > 0)
            {
                return BufferPool.Dequeue();
            }

            return new float[PageSize];
        }

        public static void Recycle(float[] buff)
        {
            BufferPool.Enqueue(buff);
        }

        public FloatStream(int totalLen)
        {
            var count = (totalLen + PageSize - 1) / PageSize;
            Buffers = new(count);
        }

        public void Add(float[] buffer)
        {
            Debug.Assert(buffer.Length == PageSize);
            Buffers.Add(buffer);
        }

        public int ReadSamples(float[] data, int position, int len)
        {
            var index1 = position / PageSize;
            var index2 = (position + len + PageSize - 1) / PageSize;
            var iEnd = position + len;
            int copyLen1 = 0;
            for (var i = index1; i < index2; i++)
            {
                if (i >= Buffers.Count)
                {
                    break;
                }

                var i1 = i * PageSize;
                var i2 = (i + 1) * PageSize;
                var copyLen = Mathf.Min(iEnd, i2) - position;
                try
                {
                    Array.Copy(Buffers[i], position - i1, data, 0, copyLen);
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                }

                copyLen1 += copyLen;
                position += copyLen;
            }

            return copyLen1;
        }

        public void Dispose()
        {
            foreach (var buffer in this.Buffers)
            {
                Recycle(buffer);
            }

            this.Buffers.Clear();
            this.Buffers = null;
        }
    }
}