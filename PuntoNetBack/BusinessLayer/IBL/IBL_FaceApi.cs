using DataAccessLayer.Helpers;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.IBL
{
    public interface IBL_FaceApi
    {
       
        public Task<IList<DetectedFace>> GetFaceListAsync(FileStream stream);


    
        public Task<IList<PersonGroup>> GetAllPersonGroupsAsync();


        public Task<IList<string>> GetAllPersonNamesAsync();


        public Task GetOrCreatePersonGroupAsync();


        public Task GetOrCreatePersonAsync(string name, ObservableCollection<ImageInfo> GroupInfos);


        public Task AddFacesToPersonAsync(IList<ImageInfo> selectedItems, ObservableCollection<ImageInfo> GroupInfos);


        
        public Task<bool> MatchFaceAsync(Guid faceId, ImageInfo newImage);

       
        public Task DisplayFacesAsync(ObservableCollection<ImageInfo> GroupInfos);


        public Task DeletePersonAsync(ObservableCollection<ImageInfo> GroupInfos,
            ObservableCollection<string> GroupNames, bool askFirst = true);


        public Task<bool> GetTrainingStatusAsync();

        public Task<IList<string>> GetFaceImagePathsAsync();


        public string ConfigurePersonName(string name);
    }
}
