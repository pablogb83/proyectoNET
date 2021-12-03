using DataAccessLayer.Helpers;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IDAL
{
    public interface IDAL_FaceApi
    {

        /// Returns all faces detected in an image stream
        /// </summary>
        /// <param name="stream">An image</param>
        /// <returns>A list of detected faces or an empty list</returns>
        public  Task<IList<DetectedFace>> GetFaceListAsync(FileStream stream);


        /// <summary>
        /// Returns all PersonGroup's associated with the Face subscription key
        /// </summary>
        /// <returns>A list of PersonGroup's or an empty list</returns>
        public Task<IList<PersonGroup>> GetAllPersonGroupsAsync();


        /// <summary>
        /// Returns all Person.Name's associated with PERSONGROUPID
        /// </summary>
        /// <returns>A list of Person.Name's or an empty list</returns>
        public  Task<IList<string>> GetAllPersonNamesAsync();


        /// <summary>
        /// Gets or creates a PersonGroup with PERSONGROUPID
        /// </summary>
        public Task GetOrCreatePersonGroupAsync();


        /// <summary>
        /// Gets or creates a PersonGroupPerson
        /// </summary>
        /// <param name="name">PersonGroupPerson.Name</param>
        /// <param name="GroupInfos">A collection specifying the file paths of images associated with <paramref name="name"/></param>
        public Task GetOrCreatePersonAsync(string name, ObservableCollection<ImageInfo> GroupInfos);


        // Each image should contain only 1 detected face; otherwise, must specify face rectangle.
        /// <summary>
        /// Adds PersistedFace's to 'personName'
        /// </summary>
        /// <param name="selectedItems">A collection specifying the file paths of images to be associated with searchedForPerson</param>
        /// <param name="GroupInfos"></param>
        public Task AddFacesToPersonAsync(IList<ImageInfo> selectedItems, ObservableCollection<ImageInfo> GroupInfos);


        /// <summary>
        /// Determines whether a given face matches searchedForPerson 
        /// </summary>
        /// <param name="faceId">PersistedFace.PersistedFaceId</param>
        /// <param name="newImage">On success, contains confidence value</param>
        /// <returns>Whether <paramref name="faceId"/> matches searchedForPerson</returns>
        public Task<bool> MatchFaceAsync(Guid faceId, ImageInfo newImage);

        /// <summary>
        /// Sets 'GroupInfos', which specifies the file paths of images associated with searchedForPerson
        /// </summary>
        /// <param name="GroupInfos">On success, contains image info associated with searchedForPerson</param>
        public  Task DisplayFacesAsync(ObservableCollection<ImageInfo> GroupInfos);

        /// <summary>
        /// Deletes searchedForPerson
        /// </summary>
        /// <param name="GroupInfos"></param>
        /// <param name="GroupNames"></param>
        /// <param name="askFirst">true to display a confirmation dialog</param>
        public  Task DeletePersonAsync(ObservableCollection<ImageInfo> GroupInfos,
            ObservableCollection<string> GroupNames, bool askFirst = true);


        // TODO: add progress indicator
        public  Task<bool> GetTrainingStatusAsync();

        // PersistedFace.UserData stores the associated image file path.
        // Returns the image file paths associated with each PersistedFace
        public  Task<IList<string>> GetFaceImagePathsAsync();


        public string ConfigurePersonName(string name);
       
    }
}
