using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace SSELauncher
{
    internal class CAppList
    {
        private CConfig m_Config;

        private string m_ConfigPath;

        [method: CompilerGenerated]
        [CompilerGenerated]
        public event EventHandler<AppModifiedEventArgs> EventAppClear;

        [method: CompilerGenerated]
        [CompilerGenerated]
        public event EventHandler<AppModifiedEventArgs> EventAppAdded;

        [method: CompilerGenerated]
        [CompilerGenerated]
        public event EventHandler<AppModifiedEventArgs> EventAppDeleted;

        public CAppList(string AppPath)
        {
            m_ConfigPath = Path.Combine(AppPath, "config.xml");
        }

        public void Load()
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(CConfig));
                using (StreamReader streamReader = new StreamReader(m_ConfigPath))
                {
                    m_Config = (CConfig)xmlSerializer.Deserialize(streamReader);
                    streamReader.Close();
                }
            }
            catch
            {
                m_Config = new CConfig();
                m_Config.LoadDefault(true);
            }
            RemoveDuplicates(ref m_Config.MasterServerAddress);
            RemoveDuplicates(ref m_Config.BroadcastAddress);
        }

        public void Save()
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(m_Config.GetType());
                using (StreamWriter streamWriter = new StreamWriter(m_ConfigPath))
                {
                    xmlSerializer.Serialize(streamWriter, m_Config);
                    streamWriter.Close();
                }
            }
            catch
            {
            }
        }

        private void RemoveDuplicates(ref List<string> unsanitized_list)
        {
            List<string> list = new List<string>();
            foreach (string current in unsanitized_list)
            {
                bool flag = false;
                using (List<string>.Enumerator enumerator2 = list.GetEnumerator())
                {
                    while (enumerator2.MoveNext())
                    {
                        if (enumerator2.Current == current)
                        {
                            flag = true;
                            break;
                        }
                    }
                }
                if (!flag)
                {
                    list.Add(current);
                }
            }
            unsanitized_list.Clear();
            unsanitized_list.AddRange(list);
        }

        public void Refresh()
        {
            EventHandler<AppModifiedEventArgs> eventAppClear = EventAppClear;
            EventHandler<AppModifiedEventArgs> eventAppAdded = EventAppAdded;
            AppModifiedEventArgs appModifiedEventArgs = new AppModifiedEventArgs();
            eventAppClear?.Invoke(this, appModifiedEventArgs);
            foreach (CApp current in m_Config.m_Apps)
            {
                if (eventAppAdded != null)
                {
                    appModifiedEventArgs.tag = null;
                    appModifiedEventArgs.app = current;
                    eventAppAdded(this, appModifiedEventArgs);
                }
            }
        }

        public CConfig GetConfig()
        {
            return m_Config;
        }

        public int AddApp(CApp app)
        {
            m_Config.m_Apps.Add(app);
            EventAppAdded?.Invoke(this, new AppModifiedEventArgs
            {
                tag = null,
                app = app
            });
            return m_Config.m_Apps.Count - 1;
        }

        public void DeleteApp(object tag)
        {
            foreach (CApp current in m_Config.m_Apps)
            {
                if (current.Tag == tag)
                {
                    EventAppDeleted?.Invoke(this, new AppModifiedEventArgs
                    {
                        tag = tag,
                        app = current
                    });
                    m_Config.m_Apps.Remove(current);
                    break;
                }
            }
        }

        public CApp GetApp(object tag)
        {
            foreach (CApp current in m_Config.m_Apps)
            {
                if (current.Tag == tag)
                {
                    return current;
                }
            }
            return null;
        }

        public CApp GetApp(string tag)
        {
            foreach (CApp current in m_Config.m_Apps)
            {
                if (current.GameName == tag)
                {
                    return current;
                }
            }
            return null;
        }

        public List<CApp> GetItems()
        {
            return m_Config.m_Apps;
        }

        public int GetCount()
        {
            return m_Config.m_Apps.Count<CApp>();
        }

        public CApp GetItem(int index)
        {
            return m_Config.m_Apps[index];
        }

        public void TagItem(int index, object obj)
        {
            m_Config.m_Apps[index].Tag = obj;
        }

        public static Image GetIcon(CApp app)
        {
            string absolutePath;
            if (!string.IsNullOrEmpty(app.IconPath))
            {
                absolutePath = CApp.GetAbsolutePath(app.IconPath);
            }
            else
            {
                absolutePath = CApp.GetAbsolutePath(app.Path);
            }
            Image result;
            try
            {
                result = Image.FromFile(absolutePath);
            }
            catch
            {
                try
                {
                    result = Icon.ExtractAssociatedIcon(absolutePath).ToBitmap();
                }
                catch
                {
                    result = null;
                }
            }
            return result;
        }
    }
}
