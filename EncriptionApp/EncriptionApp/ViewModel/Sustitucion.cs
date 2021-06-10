using EncriptionApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace EncriptionApp.ViewModel
{
    class Sustitucion : ModelBase
    {
        public ICommand PickFileCommand { get; set; }
        public ICommand DelPickerCommand { get; set; }
        public ICommand EncryptCommand { get; set; }
        public ICommand DecryptCommand { get; set; }
        public bool IsDecrypt = false;
        public string abc = "abcdefghijklmñnopqrstuvwxyzABCDEFGHIJKLMNÑOPQRSTUVWXYZ1234567890_-+,#$%&/()=¿?¡!|,.;:{}[]";
        private string _Text;
        public string Text
        {
            get => _Text;
            set
            {
                _Text = value;
                OnPropertyChanged();
            }
        }
        private string _Text2;
        public string Text2
        {
            get => _Text2;
            set
            {
                _Text2 = value;
                OnPropertyChanged();
            }
        }
        public Sustitucion()
        {

            this.EncryptCommand = new Command(Encrypt);
            this.DecryptCommand = new Command(Decrypt);
            this.PickFileCommand = new Command(PickF);
            this.DelPickerCommand = new Command(DelPick);
        }
        public async void Encrypt(object obj)
        {
            string cifrado = "";
            if (Text == null || Text == "")
            {
                await Application.Current.MainPage.DisplayAlert("Advertencia", "Debe ingresar un mensaje para poder Encriptar", "Ok");
            }
            else
            {
               
                for (int i = 0; i < Text.Length; i++)
                {
                    if (GetPosABC(Text[i]) != -1)
                    {
                        int pos = GetPosABC(Text[i]) + 3;
                        while (GetPosABC(Text[i]) + 3 >= abc.Length)
                        {
                            pos = GetPosABC(Text[i]) + 3 - abc.Length;
                        }
                        cifrado += abc[GetPosABC(Text[i]) + 3];
                    }
                    else
                    {
                        cifrado += Text[i];
                    }
                }
                Text2 = cifrado;
                Text = string.Empty;
                IsDecrypt = false;
            }
               
        }

        public async void Decrypt(object obj)
        {
            string cifrado = "";
            if (IsDecrypt)
            {
                await Application.Current.MainPage.DisplayAlert("Advertencia", "El texto ya fue desencriptado", "ok");
            }
            else
            {
                
                for (int i = 0; i < Text2.Length; i++)
                {
                    int posCaracter = GetPosABC(Text2[i]);
                    if (posCaracter != -1)
                    {
                        int pos = posCaracter - 3;
                        while (pos < 0)
                        {
                            pos += abc.Length;
                        }
                        cifrado += abc[pos];
                    }
                    else
                    {
                        cifrado += Text2[i];
                    }
                }
                Text2 = cifrado;
                IsDecrypt = true;
            }

             
        }

       public int GetPosABC(char caracter)
        {
            for (int i = 0; i < abc.Length; i++)
            {
                if (caracter == abc[i])
                {
                    return i;
                }
            }
            return -1;
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
                    Text = await reader.ReadToEndAsync();
                }
            }

        }


    }
}
