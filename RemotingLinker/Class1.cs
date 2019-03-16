using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Collections;
using System.ComponentModel;
using System.Timers;

namespace RemotingLinker
{
    public class SampleObject : MarshalByRefObject
    {

      public  Hashtable hTChatMsg = new Hashtable();
        public Hashtable update = new Hashtable();
        public ArrayList alOnlineUser = new ArrayList();
        private int key = 0;
        ArrayList list = new ArrayList();

        private String[] mess ;
       private int i = 0;
        public BackgroundWorker worker1;

        // fonxtion permettant la connexion au chat
        public bool JoinToChatRoom(string name)
        {
            // si le pseudo est deja utilise , l'acces est refusé
            if (alOnlineUser.IndexOf(name) > -1)
                return false;
            else
            {
                // le pseudo se connecte
                alOnlineUser.Add(name);
                SendMsgToSvr(name + " : est connecté");
                return true;
            }
        }
        // quitter le group
        public void LeaveChatRoom(string name)
        {
            alOnlineUser.Remove(name);
            SendMsgToSvr(name + " : a quitter le groupe");
        }

        // liste des ytilisateurs en ligne
        public ArrayList GetOnlineUser()
        {
            return alOnlineUser;
        }
        //retoune la clé attribué a l'utilisateur quand il s'est connecté
        public int CurrentKeyNo()
        {
            return key;
        }
            
        public void SendMsgToSvr(string chatMsgFromUsr)
        {
            

        
            hTChatMsg.Add(++key, chatMsgFromUsr);
            list.Add(chatMsgFromUsr);
            

        }

        public ArrayList GetMsgSvr()
        {
           
                return list;
          
        }

        // retourne le dernioer message enregistrer dans le serveur
        public string GetMsgFromSvr(int lastKey)
        {
         
            if (key > lastKey)
                return hTChatMsg[lastKey + 1].ToString();
            else
                return "";
        }

        public string GetMsgFromSv(int lastKey)
        {

            if (key > lastKey)
                return hTChatMsg[lastKey + 1].ToString();
            else
                return "";
        }

        public void Updater (Hashtable hTChatMsg)
        {
             update = hTChatMsg;
           
        }


    }
}
