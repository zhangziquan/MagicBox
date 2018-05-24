using System;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using MagicBox.Models;

namespace MagicBox.ViewModels
{
    public class MainViewModel : ViewModelBase
    {

       // private static MainViewModel viewModel;
        public MainViewModel() {
            var item = new Model();
            this.allItems.Add(item);

        }


        private ObservableCollection<Models.Model> allItems = new ObservableCollection<Models.Model>();
        public ObservableCollection<Models.Model> AllItems { get { return this.allItems; } }

        private Models.Model selectedItem = default(Models.Model);
        public Models.Model SelectedItem { get { return selectedItem; } set { this.selectedItem = value; } }

        public  void AddItem(Uri songUri, Uri photoUri, String mood, String diary, String feedback)
        {
            var item = new Model(songUri, photoUri, mood, diary, feedback);
            this.allItems.Add(item);


        }

        public void updateItem(String id, Uri songUri, Uri photoUri, String mood, String diary, String feedback)
        {
            foreach(var it in allItems)
            {
                if(it.getId() == id)
                {
                    it.songUri = songUri;
                    it.photoUri = photoUri;
                    it.mood = mood;
                    it.diary = diary;
                    it.feedback = feedback;
                    break;
                }
            }
        }

        public void deleteItem(String id)
        {
            foreach(var it in allItems)
            {
                if(it.getId() == id)
                {
                    allItems.Remove(it);
                    break;
                }
            }
        }

     


    }
}
