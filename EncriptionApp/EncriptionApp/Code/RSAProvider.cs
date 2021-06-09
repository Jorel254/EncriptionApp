using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace EncriptionApp.Code
{
    class RSAProvider
    {
        public RSACryptoServiceProvider RSAService { get; set; }
        public RSAProvider()
        {
            this.RSAService = new RSACryptoServiceProvider();
        }
        public byte[] CreatePublicKey()
        {
            string xmlPublicKey = this.RSAService.ToXmlString(false);
            return Encoding.ASCII.GetBytes(xmlPublicKey);
        }
        public byte[] CreatePrivateKey()
        {
            string xmlPrivateKey = this.RSAService.ToXmlString(true);
            return Encoding.ASCII.GetBytes(xmlPrivateKey);
        }
    }
}
