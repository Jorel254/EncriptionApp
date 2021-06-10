using System;
using System.Text;
using Xamarin.Forms;
using XamarinForms_Files.Droid;
using System.IO;
using System.Windows.Input;
using System.Security.Cryptography;
using System.Threading.Tasks;
using EncriptionApp.Models;
using EncriptionApp.Code;

[assembly: Dependency(typeof(DirectoryHelper))]
namespace EncriptionApp.ViewModel
{
    class RSAViewModel : ModelBase
    {
        FileStream fileStream;
        DirectoryHelper help;
        public ICommand CreateKeyCommand { get; set; }
        public ICommand EncyptCommand { get; set; }
        public ICommand DecyptCommand { get; set; }
        public ICommand AlertCommand { get; set; }
        private string _Text;
        private bool _isBusy;
        private string Path1;
        private string Path2;
        private string _Text2;
        public bool IsDecrypt = false;
        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }
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
        public RSAViewModel()
        {
            this.CreateKeyCommand = new Command(CreateKeys);
            this.EncyptCommand = new Command(Encrypt);
            this.DecyptCommand = new Command(Decrypt);
            this.AlertCommand = new Command(Alert);
            help = new DirectoryHelper();
            Path1 = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/LLaves" + "/PublicKey.xml";
            Path2 = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/LLaves" + "/PrivateKey.xml";
        }

        private async void Encrypt(object obj)
        {
            if (Text == null || Text == "")
            {
                await Application.Current.MainPage.DisplayAlert("Advertencia", "Debe ingresar un mensaje para poder Encriptar", "Ok");
            }
            else
            {
                if (File.Exists(Path1))
                {
                    string xmlpublickey = new StreamReader(Path1).ReadToEnd();
                    byte[] encryptData = EncriptarTexto(Text, xmlpublickey);
                    Text2 = Convert.ToBase64String(encryptData);
                    Text = string.Empty;
                    IsDecrypt = false;
                }
            }

        }
        public static byte[] EncriptarTexto(string text, string publicKey)
        {
            RSACryptoServiceProvider rSACrypto = new RSACryptoServiceProvider(4096);
            rSACrypto.FromXmlString(publicKey);
            byte[] encryptData = rSACrypto.Encrypt(Encoding.ASCII.GetBytes(text), false);
            return encryptData;
        }
        private async void Decrypt(object obj)
        {
            if (IsDecrypt)
            {
                await Application.Current.MainPage.DisplayAlert("Advertencia", "El texto ya fue desencriptado", "ok");
            }
            else
            {
                if (File.Exists(Path2))
                {
                    string xmlprivatekey = new StreamReader(Path2).ReadToEnd();
                    byte[] decryptData = DesencriptarTexto(Text2, xmlprivatekey);
                    Text2 = Encoding.ASCII.GetString(decryptData);
                    IsDecrypt = true;
                }
            }

        }
        public static byte[] DesencriptarTexto(string text, string privatekey)
        {
            RSACryptoServiceProvider rSACrypto = new RSACryptoServiceProvider(4096);
            rSACrypto.FromXmlString(privatekey);
            byte[] decryptData = rSACrypto.Decrypt(Convert.FromBase64String(text), false);
            return decryptData;
        }
        public void Generar()
        {

            RSAProvider rsa = new RSAProvider();
            string folderName = help.CreateDirectory("LLaves");
            Path1 = folderName + "/PublicKey.xml";
            if (File.Exists(Path1))
            {
                File.Delete(Path1);
            }
            fileStream = new FileStream(Path1, FileMode.Create, FileAccess.Write);
            byte[] publicBytes = rsa.CreatePublicKey();
            fileStream.Write(publicBytes, 0, publicBytes.Length);
            fileStream.Close();
            Path2 = folderName + "/PrivateKey.xml";
            if (File.Exists(Path2))
            {
                File.Delete(Path2);
            }
            fileStream = new FileStream(Path2, FileMode.Create, FileAccess.Write);
            byte[] privateBytes = rsa.CreatePrivateKey();
            fileStream.Write(privateBytes, 0, privateBytes.Length);
            fileStream.Close();
            Text2 = string.Empty;
        }
        private async void CreateKeys(object obj)
        {
            using (Acr.UserDialogs.UserDialogs.Instance.Loading("Generando"))
            {
                await Task.Run(() => Generar());
            }
            await Application.Current.MainPage.DisplayAlert("Finalizado", "Llaves generadas", "Ok");
        }


        public async void Alert()
        {
            await Application.Current.MainPage.DisplayAlert("Información", "Funcionamiento\nGenerar llaves: Genera las llaves publica y privada\nEcriptar:Encripata el texto ingresado\nDesencriptar: Desencripta el texto encriptado previamente\nPara inicia a encriptar genere las llaves\nal generar la llaves estas quedan guardadas y\nsolo si quiere generar nuevas llaves oprimalo de nuevo.\n", "Ok");
        }
    }
}
