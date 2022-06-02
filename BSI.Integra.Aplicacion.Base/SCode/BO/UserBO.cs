using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace BSI.Integra.Aplicacion.Base.BO
{
    public class UserBO : ClaseBase
    {
        private string _Username;
        private string _Password;

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username {
            get { return _Username; }
            set
            {
                _Username = value;
            }
        }
        public string Password {
            get { return _Password; }
            set
            {
                _Password = value;
            }
        }
    }

    public class UserValidator : AbstractValidator<UserBO>
    {
        public UserValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("La Fecha no puede ser nulo o vacio");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("La IP no puede ser nulo o vacio");
            RuleFor(x => x.Username).NotEmpty().WithMessage("La Fecha no puede ser nulo o vacio");
            RuleFor(x => x.Password).NotEmpty().WithMessage("El Password no puede ser nulo o vacio");
            RuleFor(x => x.Password).MinimumLength(5).WithMessage("El Password debe contener almenos 5 caracteres");
        }
    }

    public interface IUser
    {
        bool validarUser(UserBO model);

        UserBO Authenticate(string username, string password);
        IEnumerable<UserBO> GetAll();
        UserBO GetById(int id);
        UserBO Create(UserBO user, string password);
        void Update(UserBO user, string password = null);
    }

    public class UserImpl : IUser
    {
        private readonly IValidator<UserBO> _validatorUserBO;

        public UserImpl(IValidator<UserBO> validatorLogsBO)
        {
            _validatorUserBO = validatorLogsBO;
            //_context = context;
        }

        public bool validarUser(UserBO model)
        {
            string mensaje = string.Empty;
            int nroErrores = 0;

            var ValidacionResultado = _validatorUserBO.Validate(model);
            if (ValidacionResultado.IsValid)
            {
                return true;
            }
            else
            {
                nroErrores = ValidacionResultado.Errors.Count;
                foreach (var item in ValidacionResultado.Errors)
                {
                    mensaje += item.ErrorCode.ToString() + ": " + item.ErrorMessage.ToString() + ", ";
                }
                mensaje += "NroErrores:" + nroErrores.ToString();
                return false;
            }

        }

        public UserBO Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = new UserBO();
            //var user = _context.Users.SingleOrDefault(x => x.Username == username);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            //if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            //    return null;

            // authentication successful
            return user;
        }

        public IEnumerable<UserBO> GetAll()
        {
            //return _context.Users;
            return null;
        }

        public UserBO GetById(int id)
        {
            //return _context.Users.Find(id);
            return null;
        }

        public UserBO Create(UserBO user, string password)
        {
            // validation 
            //if (_context.Users.Any(x => x.Username == user.Username))
            //    throw new AppException("Username \"" + user.Username + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            //user.PasswordHash = passwordHash;
            //user.PasswordSalt = passwordSalt;

            //_context.Users.Add(user);
            //_context.SaveChanges();

            return user;
        }

        public void Update(UserBO userParam, string password = null)
        {
            //var user = _context.Users.Find(userParam.Id);

            //if (user == null)
            //    throw new AppException("Usuario no encontrado");

            //if (userParam.Username != user.Username)
            //{
            //    if (_context.Users.Any(x => x.Username == userParam.Username))
            //        throw new AppException("Username " + userParam.Username + " ya está registrado");
            //}
            

            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                //user.PasswordHash = passwordHash;
                //user.PasswordSalt = passwordSalt;
            }

            //_context.Users.Update(user);
            //_context.SaveChanges();
        }

        // private helper methods
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("El valor no puede estar vacío o en blanco.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Longitud de hash de contraseña no válida (se esperan 64 bytes).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Longitud de contraseña no válida (se esperan 128 bytes).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}
