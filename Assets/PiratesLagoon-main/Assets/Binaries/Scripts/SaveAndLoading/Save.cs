using UnityEngine;
using System.IO;
using System;
using System.Security;
using System.Text;
using System.Security.Cryptography;

namespace Managers
{
    //[Serializable]
    //public class SaveData
    //{
    //    public string Name;
    //    public int Level;
    //    public float Gold;
    //}

    [Serializable]
    public class LockedData
    {
        public PlayerStats Data;
        public string Hash;
    }

    public class Save : MonoBehaviour
    {
        #region Champs
        //INSPECTOR

        //PRIVATES
        //public PlayerStats _playerStats;
        DateTime _localDate;
        const string PASSWORD = "!Pyc2024aJ!";
        string SaveFilename() => Application.persistentDataPath + "/Save" + "/SaveFile.Sav";
        //PUBLICS
        #endregion
        #region Default Informations
        void Reset()
        {
        
        }
        #endregion
        #region Unity LifeCycle
        #endregion
        #region Methods
        string GetSHA256(string text)
        {
            var textToBytes = Encoding.UTF8.GetBytes(text);
            var mySHA = SHA256.Create();
            var hashValue = mySHA.ComputeHash(textToBytes);

            return GetHexStringFromHash(hashValue);

            string GetHexStringFromHash(byte[] hash) => BitConverter.ToString(hash).Replace("-", "");
        }

        public void Saving(PlayerStats data)
        {
            // Windows = C:\Users\<user>\AppData\LocalLow\<company name>
            // WebGL = /idbfs/<md5 hash of data path>
            // Linux = $XDG_CONFIG_HOME/unity3d or $HOME/.config/unity3d
            // IOS = /var/mobile/Containers/Data/Application/<guid>/Documents
            // Android = /storage/emulated/<userid>/Android/data/<packagename>/files
            // MacOs = ~/Library/Application Support/company name/product name
            string computDirectory = Application.persistentDataPath + "/pyc/save";
            if (!Directory.Exists(computDirectory))
            {
                Directory.CreateDirectory(computDirectory);
            }

            //
            LockedData lockedData = CreateLockedData(data);
            string json = JsonUtility.ToJson(lockedData);
            lockedData.Hash = GetSHA256(json);
            json = JsonUtility.ToJson(lockedData);

            var fileStream = new FileStream(SaveFilename(), FileMode.Create);
            using (var writer = new StreamWriter(fileStream))
            {
                writer.Write(json);
            }

            LockedData CreateLockedData(PlayerStats saveData) => new() { Data = saveData, Hash = PASSWORD };

            //_localDate = DateTime.Now;
            //File.WriteAllText(computDirectory + $"/save.txt", Json);
        }

        bool TryLoadFile(out PlayerStats data)
        {
            data = null;
            var lockedData = new LockedData();

            if (File.Exists(SaveFilename()))
                using (var reader = new StreamReader(SaveFilename()))
                {
                    var json = reader.ReadToEnd();

                    if (CheckData(json))
                    {
                        JsonUtility.FromJsonOverwrite(json, lockedData);
                        data = lockedData.Data;
                        return true;
                    }
                }

            return false;

            bool CheckData(string json)
            {
                var dataForCheck = new LockedData();
                JsonUtility.FromJsonOverwrite(json, dataForCheck);

                var hash = dataForCheck.Hash;

                dataForCheck.Hash = PASSWORD;

                var jsonWithPassword = JsonUtility.ToJson(dataForCheck);

                var newHash = GetSHA256(jsonWithPassword);

                return hash == newHash;
            }
        }

        //public void Loading(out PlayerStats data)
        //{
        //    string savedFilePath = Application.persistentDataPath + "/pyc/save/save.txt";
        //    if (File.Exists(savedFilePath))
        //    {
        //        string Json = File.ReadAllText(savedFilePath);
        //        _playerStats = JsonUtility.FromJson<PlayerStats>(Json);
        //    }
        //    else
        //    {
        //        Debug.Log("Le fichier de sauvegarde n'existe pas !");
        //    }
        //}
        #endregion
        #region Coroutines 
        #endregion
    }
}