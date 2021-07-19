using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Messenger
{

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class ServiceChat : IServiceChat
    {
        private List<ServerUser> _users = new List<ServerUser>();
        private int _nextId = 1;

        public int Connect(string name)
        {
            ServerUser user = new ServerUser()
            {
                Id = _nextId,
                Name = name,
                operationContext = OperationContext.Current
            };
            _nextId++;

            SendMsg(user.Name + " подключился к чату!");
            _users.Add(user);
            return user.Id;
        }

        public void Disconnect(int id)
        {
            var user = _users.FirstOrDefault(i => i.Id == id);
            if (user != null)
            {
                _users.Remove(user);
                SendMsg(user.Name + " покинул чат!");
            }
        }
  
        public void SendMsg(string msg, int id = 0)
        {
            foreach (var item in _users)
            {
                string answer = DateTime.Now.ToShortTimeString();

                var user = _users.FirstOrDefault(i => i.Id == id);
                if (user != null)
                {
                    answer += " " + user.Name;
                }

                answer += ": " + msg;

                item.operationContext.GetCallbackChannel<IServerChatCallback>().MsgCallback(answer);
            }
        }
    }
}
