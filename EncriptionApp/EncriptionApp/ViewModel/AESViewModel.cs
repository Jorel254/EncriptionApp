using EncriptionApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EncriptionApp.ViewModel
{
    class AESViewModel : ModelBase
    {
        public ICommand PickFileCommand { get; set; }
        public ICommand DelPickerCommand { get; set; }
        public ICommand EncyptCommand { get; set; }
        public ICommand DecyptCommand { get; set; }
        private string _Text;
        private string _Text2;
        byte[] DecryptB;
        byte[] EncryptB;
        public bool IsDecrypt = false;
        AesCryptoServiceProvider aes;
        public string Text
        {
            get => _Text;
            set
            {
                _Text = value;
                OnPropertyChanged();
            }
        }
        public string Text2
        {
            get => _Text2;
            set
            {
                _Text2 = value;
                OnPropertyChanged();
            }
        }


        public AESViewModel()
        {
            aes = new AesCryptoServiceProvider();
            aes.GenerateIV();
            aes.GenerateKey();
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;
            this.PickFileCommand = new Command(PickF);
            this.DelPickerCommand = new Command(DelPick);
            this.EncyptCommand = new Command(Encrypt);
            this.DecyptCommand = new Command(Decrypt);
        }
        private async void Decrypt(object obj)
        {
            if (IsDecrypt)
            {
                await Application.Current.MainPage.DisplayAlert("Advertencia", "El texto ya fue desencriptado", "ok");
            }
            else
            {
                DecryptB = DesencriptarTexto(aes, EncryptB);
                Text2 = Encoding.UTF8.GetString(DecryptB);
                IsDecrypt = true;
            }

        }
        private void Encrypt(object obj)
        {
            EncryptB = EncriptarTexto(aes, Encoding.UTF8.GetBytes(Text));
            Text2 = BitConverter.ToString(EncryptB).Replace("-", " ");
            Text = string.Empty;
            IsDecrypt = false;
        }
        public static byte[] DesencriptarTexto(AesCryptoServiceProvider aes, byte[] text)
        {
            using (var memory = new MemoryStream())
            {
                CryptoStream cryptoStream = null;
                cryptoStream = new CryptoStream(memory, aes.CreateDecryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(text, 0, text.Length);
                cryptoStream.FlushFinalBlock();
                return memory.ToArray();
            }
        }
        public static byte[] EncriptarTexto(AesCryptoServiceProvider aes, byte[] text)
        {
            using (var memory = new MemoryStream())
            {
                CryptoStream cryptoStream = null;
                cryptoStream = new CryptoStream(memory, aes.CreateEncryptor(), CryptoStreamMode.Write);
                cryptoStream.Write(text, 0, text.Length);
                cryptoStream.FlushFinalBlock();
                return memory.ToArray();
            }
        }
        private void DelPick(object obj)
        {
            Text = string.Empty;
        }
        private async void PickF(object obj)
        {
            var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
                {DevicePlatform.Android, new[] { "text/plain" } }
            });
            var file = await FilePicker.PickAsync(
            new PickOptions
            {
                FileTypes = customFileType
            });
            
            using (Stream stream = await file.OpenReadAsync())
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    Text =await reader.ReadToEndAsync();
                }
            }
        }

    }
}
