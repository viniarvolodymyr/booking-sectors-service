using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SoftServe.BookingSectors.WebAPI.BLL.DTO;
using SoftServe.BookingSectors.WebAPI.BLL.ErrorHandling;
using SoftServe.BookingSectors.WebAPI.BLL.Helpers;
using SoftServe.BookingSectors.WebAPI.BLL.Helpers.LoggerManager;
using SoftServe.BookingSectors.WebAPI.BLL.Services.Interfaces;
using SoftServe.BookingSectors.WebAPI.DAL.Models;
using SoftServe.BookingSectors.WebAPI.DAL.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography;

namespace SoftServe.BookingSectors.WebAPI.BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork database;
        private readonly IMapper mapper;
        private readonly ILoggerManager logger;

        public UserService(IUnitOfWork database, IMapper mapper, ILoggerManager logger)
        {
            this.database = database;
            this.mapper = mapper;
            this.logger = logger;
        }

        #region Get
        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await database.UserRepository.GetAllEntitiesAsync();

            return mapper.Map<IEnumerable<User>, List<UserDTO>>(users);
        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            var entity = await database.UserRepository.GetEntityByIdAsync(id);
            if (entity == null)
            {
                return null;
            }

            var dto = mapper.Map<User, UserDTO>(entity);

            if (entity.Photo != null)
            {
                dto.Photo = Convert.ToBase64String(entity.Photo);
            }

            return dto;
        }

        public async Task<UserDTO> GetUserByPhoneAsync(string phone)
        {
            var user = await database.UserRepository
                .GetByCondition(x => x.Phone == phone)
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound,
                    $"User with phone number: {phone} not found when trying to get entity.");
            }

            return mapper.Map<User, UserDTO>(user);
        }

        public async Task<string> GetUserPhotoById(int id)
        {
            var entity = await database.UserRepository.GetEntityByIdAsync(id);
            if (entity == null)
            {
                return null;
            }

            var b64 = Convert.ToBase64String(entity.Photo);

            using (var ms = new MemoryStream(entity.Photo))
            {
                File.WriteAllBytes("test.jpg", entity.Photo);
                var file = new FormFile(ms, 0, ms.Length, "file.jpg", "file")
                {
                    Headers = new HeaderDictionary(),
                    ContentType = "image/jpeg",

                };

                return b64;
            }
        }
        #endregion


        #region Update

        public async Task<UserDTO> UpdateUserById(int id, UserDTO userDTO)
        {
            var existedUser = await database.UserRepository.GetEntityByIdAsync(id);
            if (existedUser == null)
            {
                return null;
            }

            existedUser.Firstname = userDTO.Firstname;
            existedUser.Lastname = userDTO.Lastname;
            existedUser.Phone = userDTO.Phone;
            existedUser.Email = userDTO.Email;

            //Але сумнівно що можна буде обновити хоч колись за раз email i номер без перепровірок.
            //сумнівний метод взагалі получається.
            //і взагалі всі поля треба провіряти на заповненість а не заносити все підряд.
            //мол
            // existedUser.Lastname = (userDTO.Lastname == $"String" || userDTO.Lastname == existedUser.Lastname) ? 
            //    userDTO.Lastname :
            //    existedUser.Lastname;

            var updatedUser = database.UserRepository.UpdateEntity(existedUser);
            bool isSaved = await database.SaveAsync();

            return isSaved ?
                mapper.Map<User, UserDTO>(updatedUser) :
                null;
        }

        public async Task<UserDTO> UpdateUserPassById(int id, string password)
        {
            var existedUser = await database.UserRepository.GetEntityByIdAsync(id);
            if (existedUser == null)
            {
                return null;
            }

            existedUser.Password = SHA256Hash.Compute(password);

            var updatedUser = database.UserRepository.UpdateEntity(existedUser);
            bool isSaved = await database.SaveAsync();

            return isSaved ?
                mapper.Map<User, UserDTO>(updatedUser) :
                null;
        }

        public async Task<UserDTO> UpdateUserPhotoById(int id, IFormFile formFile)
        {
            var existedUser = await database.UserRepository.GetEntityByIdAsync(id);
            if (existedUser == null)
            {
                return null;
            }

            using (var memoryStream = new MemoryStream())
            {
                await formFile.CopyToAsync(memoryStream);

                // Upload the file if less than 2 MB
                if (memoryStream.Length < 2097152)
                {
                    existedUser.Photo = memoryStream.ToArray();
                    var updatedUser = database.UserRepository.UpdateEntity(existedUser);
                    bool isSaved = await database.SaveAsync();

                    return isSaved ?
                        mapper.Map<User, UserDTO>(updatedUser) :
                        null;
                }


                return null;
                //  ModelState.AddModelError("File", "The file is too large.");
            }
        }

        #endregion

        public async Task<UserDTO> DeleteUserByIdAsync(int id)
        {
            var user = await database.UserRepository.DeleteEntityByIdAsync(id);
            if (user == null)
            {
                return null;
            }
            bool isSaved = await database.SaveAsync();


            return (isSaved) ?
                mapper.Map<User, UserDTO>(user) :
                null;
        }

        #region Password
        public async Task<bool> ResetPassword(UserDTO userDTO)
        { 
        //{ byte[] encrypted;
        //    string x;
        //    using (AesManaged myAes = new AesManaged())
        //    {
        //        // Encrypt the string to an array of bytes.
        //        encrypted = EncryptStringToBytes_Aes(user.Id.ToString(), myAes.Key, myAes.IV);
        //        x = Convert.ToBase64String(encrypted);
        //        byte[] bdec = Convert.FromBase64String(x);
        //        string result = DecryptStringFromBytes_Aes(bdec, myAes.Key, myAes.IV);
        //        // Decrypt the bytes to a string.
        //        string roundtrip = DecryptStringFromBytes_Aes(encrypted, myAes.Key, myAes.IV);

        //        //Display the original data and the decrypted data.
             
        //    }
        //    using (AesManaged myAes = new AesManaged())
        //    {
           
        //        byte[] bdec = Convert.FromBase64String(x);
        //        string result = DecryptStringFromBytes_Aes(bdec, myAes.Key, myAes.IV);
        //        // Decrypt the bytes to a string.
        //        string roundtrip = DecryptStringFromBytes_Aes(encrypted, myAes.Key, myAes.IV);

        //        //Display the original data and the decrypted data.
        //        // Console.WriteLine("Original:   {0}", user.Id.ToString());
        //        //Console.WriteLine("Round Trip: {0}", roundtrip);
        //    }
            var existedUser = await database.UserRepository.GetEntityByIdAsync(userDTO.Id);
            if (existedUser == null)
            {
                return false;
            }

            string newPass = RandomNumbers.Generate();

            EmailSender sender = new EmailSender($"Hello, {userDTO.Firstname}." +
                                             $" Your new password: {Environment.NewLine}" +
                                             $" {newPass} {Environment.NewLine}. You can change it in your profile. {Environment.NewLine} Have a nice day :) ");

            await sender.SendAsync("Reset password on TridentLake",
                userDTO.Email,
                $"{userDTO.Lastname} {userDTO.Firstname}");

            existedUser.Password = SHA256Hash.Compute(newPass);

            var updatedUser = database.UserRepository.UpdateEntity(existedUser);
            bool isSaved = await database.SaveAsync();

            return isSaved ?
                true :
                false;
            //In the future return Error on problems with tokens
        }

        public async Task<bool> CheckPasswords(string password, int id)
        {
            var entity = await database.UserRepository.GetEntityByIdAsync(id);
            byte[] passToCheck = SHA256Hash.Compute(password);

            return entity.Password.SequenceEqual(passToCheck);
        }
        #endregion


        static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            // Create an AesManaged object
            // with the specified key and IV.
            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create an encryptor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }


            // Return the encrypted bytes from the memory stream.
            return encrypted;

        }

        static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an AesManaged object
            // with the specified key and IV.
            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decryptor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }
    }
}
