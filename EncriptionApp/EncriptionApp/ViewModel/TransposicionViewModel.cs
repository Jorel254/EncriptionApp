using EncriptionApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace EncriptionApp.ViewModel
{
    class TransposicionViewModel : ModelBase
    {
        public ICommand EncryptCommand { get; set; }
        public ICommand DecryptCommand { get; set; }
        public ICommand EncryptCommandB { get; set; }
        public ICommand DecryptCommandB { get; set; }
        public bool IsDecrypt = false;
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
        public TransposicionViewModel()
        {
            this.EncryptCommand = new Command(Encrypt);
           this.DecryptCommand = new Command(Decrypt);
            this.EncryptCommandB = new Command(EncryptBinary);
            this.DecryptCommandB = new Command(DecryptBinary);
        }

        private async void Encrypt(object obj)
        {
            if (Text == null || Text == "")
            {
                await Application.Current.MainPage.DisplayAlert("Advertencia", "Debe ingresar un mensaje para poder Encriptar", "Ok");
            }
            else
            {
                for (int i = Text.Length - 1; i >= 0; i--)
                {
                    Text2 += Text[i];
                }
                Text2 = Text2;
                Text = string.Empty;
                IsDecrypt = false;
            }
        }
        private async void Decrypt(object obj)
        {
            if (IsDecrypt)
            {
                await Application.Current.MainPage.DisplayAlert("Advertencia", "El texto ya fue desencriptado", "ok");
            }
            else
            {
                string var = "";
                for (int i = Text2.Length - 1; i >= 0; i--)
                {
                    var += Text2[i];
                }
                Text2 = string.Empty;
                Text2 = var;
                IsDecrypt = true;
            }
        }
        public async void Alert()
        {
            await Application.Current.MainPage.DisplayAlert("Información", "Funcionamiento\nGenerar llaves: Genera las llaves publica y privadaEcriptar:Encripata el texto ingresado\nDesencriptar: Desencripta el texto encriptado previamente\nPara inicia a encriptar genere las llaves\nal generar la llaves estas quedan guardadas y\nsolo si quiere generar nuevas llaves oprimalo de nuevo.\n", "Ok");
        }
        private async void EncryptBinary(object obj)
        {
            if (Text == null || Text == "")
            {
                await Application.Current.MainPage.DisplayAlert("Advertencia", "Debe ingresar un mensaje para poder Encriptar", "Ok");
            }
            else
            {
                string binaria = "";
                int i = 0;
                while (i < Text.Length)
                {
                    if ((i + 1) < Text.Length)
                    {
                        binaria = binaria + Text[i + 1].ToString() + Text[i].ToString();
                    }
                    else
                    {
                        binaria += Text[Text.Length - 1].ToString();
                    }
                    i += 2;
                }
                Text2 = binaria;
                Text = string.Empty;
                IsDecrypt = false;
            }
        }
        private async void DecryptBinary(object obj)
        {
            if (IsDecrypt)
            {
                await Application.Current.MainPage.DisplayAlert("Advertencia", "El texto ya fue desencriptado", "ok");
            }
            else
            {
                string binaria = "";
                int i = 0;
                while (i < Text2.Length)
                {
                    if ((i + 1) < Text2.Length)
                    {
                        binaria = binaria + Text2[i + 1].ToString() + Text2[i].ToString();
                    }
                    else
                    {
                        binaria += Text2[Text2.Length - 1].ToString();
                    }
                    i += 2;
                }
                Text2 = binaria;
                IsDecrypt = true;
            }
               
        }
    }
}