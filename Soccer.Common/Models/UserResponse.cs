﻿namespace Soccer.Common.Models
{
    public class UserResponse
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string Document { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Address { get; set; }

        public string PicturePath { get; set; }

        public UserType UserType { get; set; }

        public TeamResponse Team { get; set; }

        public string FullName => $"{FirstName} {LastName}";

        public string FullNameWithDocument => $"{FirstName} {LastName} - {Document}";

        public string PictureFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(this.PicturePath))
                {
                    return "torneo";
                }

                return $"https://soccerapp.azurewebsites.net{this.PicturePath.Substring(1)}";
            }

        }
    }
}
