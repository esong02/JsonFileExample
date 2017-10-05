using System;
using System.IO;
using System.ComponentModel;
using System.Windows.Input;
using System.Reflection;

using Xamarin.Forms;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using JsonFileExample.Interfaces;
using JsonFileExample.Models;

namespace JsonFileExample.ViewModels
{
    public class ExampleVM : INotifyPropertyChanged
    {
        public double version;
        public string serviceurl;
        public int phonenumber;
        public string about;
        Settings settings = new Settings();
        string fileName = "Settings1.json";
        string resourceName = "JsonFileExample.Settings.json";

        public double Version
        {
            set{
                if (version != value){
                    version = value;
                    OnPropertyChanged("Version");
                    Write();
                }
            }
            get{
                return version;
            }
        }

		public string ServiceURL
		{
			set
			{
				if (serviceurl != value)
				{
					serviceurl = value;
					OnPropertyChanged("ServiceURL");
                    Write();
				}
			}
			get
			{
				return serviceurl;
			}
		}

		public int PhoneNumber
		{
			set
			{
				if (phonenumber != value)
				{
					phonenumber = value;
					OnPropertyChanged("PhoneNumber");
                    Write();
				}
			}
			get
			{
				return phonenumber;
			}
		}

		public string About
		{
			set
			{
				if (about != value)
				{
					about = value;
					OnPropertyChanged("About");
                    Write();
				}
			}
			get
			{
				return about;
			}
		}

        public ICommand SaveCommand { get; protected set; }

        void Write(){
            settings.Version = Version;
            settings.About = About;
            settings.PhoneNumber = PhoneNumber;
            settings.ServiceUrl = ServiceURL;
        }

        void ReadFromResource()
        {
            var assembly = typeof(JsonFileExamplePage).GetTypeInfo().Assembly;
            Stream stream = assembly.GetManifestResourceStream(resourceName);

            using (var reader = new System.IO.StreamReader(stream))
            {
                var json = reader.ReadToEnd();
                settings = JsonConvert.DeserializeObject<Settings>(json);
				version = settings.Version;
				phonenumber = settings.PhoneNumber;
				serviceurl = settings.ServiceUrl;
				about = settings.About;
            }
        }

		void ReadToFile()
		{
            var file = DependencyService.Get<ISaveAndLoad>().LoadText(fileName);
            settings = JsonConvert.DeserializeObject<Settings>(file);
            version = settings.Version;
            phonenumber = settings.PhoneNumber;
            serviceurl = settings.ServiceUrl;
            about = settings.About;
        }

        void SaveToFile(){
            var json = JsonConvert.SerializeObject(settings);
            DependencyService.Get<ISaveAndLoad>().SaveText(fileName, json);
        }

        public ExampleVM(){

            this.SaveCommand = new Command((obj) => 
            {  
                SaveToFile();
            });

            //ReadFromResource();
            if(DependencyService.Get<ISaveAndLoad>().FindFile(fileName)){
                ReadToFile();
            }else{
                Init();
            }
        }

        void Init()
        {
			settings = new Settings
			{
				Version = 0.0,
				PhoneNumber = 0,
				ServiceUrl = "",
				About = ""
			};
            SaveToFile();
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null){
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
