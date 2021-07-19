using Client.ServiceReference1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Collections.ObjectModel;

namespace Client.ViewModel
{
    class ChatViewModel : IServiceChatCallback, INotifyPropertyChanged
    {
        private ServiceChatClient _client;

        public bool IsConnected;
        private string _connectBtnName;
        public string ConnectBtnName
        {
            get => _connectBtnName;
            set
            {
                _connectBtnName = value;
                OnPropertyChanged(nameof(ConnectBtnName));
            }
        }

        private bool _isInputNameEnabled;
        public bool IsInputNameEnabled
        {
            get => _isInputNameEnabled;
            set
            {
                _isInputNameEnabled = value;
                OnPropertyChanged(nameof(IsInputNameEnabled));
            }
        }

        private string _inputName;
        public string InputName
        {
            get => _inputName;
            set
            {
                _inputName = value;
                OnPropertyChanged(nameof(InputName));
            }
        }
        public int Id;

        public ObservableCollection<string> Messages { get; set; }

        private string _inputMessage;
        public string InputMessage
        {
            get => _inputMessage;
            set
            {
                _inputMessage = value;
                OnPropertyChanged(nameof(InputMessage));
            }
        }

        public ChatViewModel()
        {
            ConnectBtnName = "Connect";
            IsInputNameEnabled = true;
            Messages = new ObservableCollection<string>();

        }

        

        public void ConnectUser()
        {
            if (!IsConnected)
            {
                _client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this));
                Id = _client.Connect(InputName);
                IsInputNameEnabled = false;
                ConnectBtnName = "Disconnect";
                IsConnected = true;
                
            }
        }

        public void DisconnectUser()
        {
            if (IsConnected)
            {
                _client.Disconnect(Id);
                _client = null;
                IsInputNameEnabled = true;
                ConnectBtnName = "Connect";
                IsConnected = false;
               
            }
        }



        public void SendMessage(string msg, int id)
        {
            if (_client != null)
            {
                _client.SendMsg(msg, id);
                InputMessage = null;
                
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
        }

        public void MsgCallback(string msg)
        {
            Messages.Add(msg);
            
        }
    }
}
