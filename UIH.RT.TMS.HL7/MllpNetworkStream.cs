#region License

// Copyright (c) 2011 - 2013, United-Imaging Inc.
// All rights reserved.
// http://www.united-imaging.com

#endregion

﻿using System;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;

namespace UIH.RT.TMS.HL7
{
    public class MllpNetworkStream : NetworkStream
    {
        #region Private Fileds

        private const byte StartOfMessage = 0x0b;

        private const byte FirstEndCharacter = 0x1C;

        private const byte LastEndCharacter = 0x0D;

        private static readonly byte[] EndOfMessageContents = { 0x1c, 0x0d };

        private bool _endOfMessage = true;

        private MemoryStream _readBuffer = new MemoryStream();

        #endregion

        #region Public Constructor

        public MllpNetworkStream(Socket socket)
            : base(socket)
        {
        }

        public MllpNetworkStream(Socket socket, bool owner)
            : base(socket, owner)
        {
        }

        #endregion

        public override int ReadByte()
        {
            int b = base.ReadByte();
            if (b == -1)
            {
                throw new Exception("unexpected end of stream.");
            }

            if (b != FirstEndCharacter)
            {
                return b;
            }

            EndOfMessage();
            return -1;
        }

        public byte[] ReadMessage()
        {
            if (!HasMoreInput())
            {
                return null;
            }

            _readBuffer = new MemoryStream();
            do
            {
                int b = this.ReadByte();
                if (b == -1)
                {
                    break;
                }

                _readBuffer.WriteByte((byte)b);
            }
            while (true);

            return _readBuffer.ToArray();
        }

        public void WriteMessage(byte[] buf, int off, int len)
        {
            this.WriteByte(StartOfMessage);
            this.Write(buf, off, len);
            this.Write(EndOfMessageContents, 0, 2);
            this.Flush();
        }

        #region Private Methods

        private void EndOfMessage()
        {
            int b = base.ReadByte();
            if (b != LastEndCharacter)
            {
                throw new Exception("First end char 0x1C is not continue with second end char 0x0D");
            }

            _endOfMessage = true;
        }

        private bool HasMoreInput()
        {
            if (!_endOfMessage)
            {
                throw new Exception();
            }

            int b = this.ReadByte();
            if (b == -1)
            {
                return false;
            }

            if (b != StartOfMessage)
            {
                throw new Exception("Missing the start filed 0x0b");
            }

            _endOfMessage = false;
            return true;
        }

        #endregion
    }

    public class SslMllpNetworkSream : SslStream
    {
        #region Private Fileds

        private const byte StartOfMessage = 0x0b;

        private const byte FirstEndCharacter = 0x1C;

        private const byte LastEndCharacter = 0x0D;

        private static readonly byte[] EndOfMessageContents = { 0x1c, 0x0d };

        private bool _endOfMessage = true;

        private MemoryStream _readBuffer = new MemoryStream();

        #endregion

        public SslMllpNetworkSream(Stream innerStream)
            : base(innerStream)
        {
        }

        public SslMllpNetworkSream(Stream innerStream, bool leaveInnerStreamOpen)
            : base(innerStream, leaveInnerStreamOpen)
        {
        }

        public SslMllpNetworkSream(
            Stream innerStream,
            bool leaveInnerStreamOpen,
            RemoteCertificateValidationCallback userCertificateValidationCallback)
            : base(innerStream, leaveInnerStreamOpen, userCertificateValidationCallback)
        {
        }

        public SslMllpNetworkSream(
            Stream innerStream,
            bool leaveInnerStreamOpen,
            RemoteCertificateValidationCallback userCertificateValidationCallback,
            LocalCertificateSelectionCallback userCertificateSelectionCallback)
            : base(
                innerStream, leaveInnerStreamOpen, userCertificateValidationCallback, userCertificateSelectionCallback)
        {
        }

        public SslMllpNetworkSream(
            Stream innerStream,
            bool leaveInnerStreamOpen,
            RemoteCertificateValidationCallback userCertificateValidationCallback,
            LocalCertificateSelectionCallback userCertificateSelectionCallback,
            EncryptionPolicy encryptionPolicy)
            : base(
                innerStream,
                leaveInnerStreamOpen,
                userCertificateValidationCallback,
                userCertificateSelectionCallback,
                encryptionPolicy)
        {
        }

        public override int ReadByte()
        {
            int b = base.ReadByte();
            if (b == -1)
            {
                throw new Exception("unexpected end of stream.");
            }

            if (b != FirstEndCharacter)
            {
                return b;
            }

            EndOfMessage();
            return -1;
        }

        public byte[] ReadMessage()
        {
            if (!HasMoreInput())
            {
                return null;
            }

            _readBuffer = new MemoryStream();
            do
            {
                int b = this.ReadByte();
                if (b == -1)
                {
                    break;
                }

                _readBuffer.WriteByte((byte)b);
            }
            while (true);

            return _readBuffer.ToArray();
        }

        public void WriteMessage(byte[] buf, int off, int len)
        {
            this.WriteByte(StartOfMessage);
            this.Write(buf, off, len);
            this.Write(EndOfMessageContents, 0, 2);
            this.Flush();
        }

        #region Private Methods

        private void EndOfMessage()
        {
            int b = base.ReadByte();
            if (b != LastEndCharacter)
            {
                throw new Exception("First end char 0x1C is not continue with second end char 0x0D");
            }

            _endOfMessage = true;
        }

        private bool HasMoreInput()
        {
            if (!_endOfMessage)
            {
                throw new Exception();
            }

            int b = this.ReadByte();
            if (b == -1)
            {
                return false;
            }

            if (b != StartOfMessage)
            {
                throw new Exception("Missing the start filed 0x0b");
            }

            _endOfMessage = false;
            return true;
        }

        #endregion
    }
}