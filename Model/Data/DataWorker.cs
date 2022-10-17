
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BusinessManager.Model.Data
{
    class DataWorker
    {

        public static string ChangeUserPassword(string newPassword, string email)
        {
            string result = "Вами введён неверный старый пароль";
            using (ApplicationContext db = new ApplicationContext())
            {
                //check user is exist
                User user = db.Users.FirstOrDefault(p => p.Email == email);
                if (user != null)
                {
                    user.Password = GetHash(newPassword);
                    db.SaveChanges();
                    result = "Сделано! Изменения сохранены!";
                }
            }
            return result;
        }

        public static bool UserExist(string email)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                //проверяем сущесвует ли отдел
                bool checkIsExist = db.Users.Any(el => el.Email == email);
                return checkIsExist;
            }
        }
        /// хеширование 
        public static string GetHash(string input)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

            return Convert.ToBase64String(hash);
        }
        #region Все списки
        //получить всех пользователей 
        public static ObservableCollection<User> GetAllUsers()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Users.ToList();
                ObservableCollection<User> res = new ObservableCollection<User>(result);
                return res;
            }
        }

        //получить всех сообщений
        public static ObservableCollection<Messages> GetAllMessages()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Messages.ToList();
                ObservableCollection<Messages> res = new ObservableCollection<Messages>(result);
                return res;
            }
        }

        public static ObservableCollection<User> GetAllUsersById()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<User> result = db.Users.Where(p=> p.Email==p.Email).ToList();
                ObservableCollection<User> res = new ObservableCollection<User>(result);
                return res;
            }
        }
        //получить все позиции
        public static User GetAllUsersByID(int userID)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Users.FirstOrDefault(p => p.User_Id == userID);
                return result;
            }
        }
        //получить всех сотрудников
        public static ObservableCollection<Projects> GetAllProjects()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Projects.ToList();
                ObservableCollection<Projects> f = new ObservableCollection<Projects>(result);
                return f;
            }
        }

        public static List<Payment> GetAllPayment()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Payment.ToList();
                return result;
            }
        }

    
  
        public static List<LegalEntity> GetAllLegalEntity()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.LegalEntity.ToList();
                return result;
            }
        }
    
       
        public static List<Entrance> GetAllEntrance()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Entrance.ToList();
                return result;
            }
        }
        public static List<Account> GetAllAccount()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Account.ToList();
                return result;
            }
        }
        #endregion

        #region Создание всех сущностей
        //создать пользователя
        public static string CreateUser(string name, string password, string email)
        {
            string result = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
             
                //проверяем сущесвует ли пользователь
                bool checkIsExist = db.Users.Any(el => el.Email == email);
                if (!checkIsExist)
                {
                    User newUser = new User { Name = name, Password = GetHash(password), Email = email};
               
                    db.Users.Add(newUser);
                    db.SaveChanges();
                    result = "Сделано!";
                }
                return result;
            }
        }

        //создать пользователя
        public static string CreateEntranceArticle(string title)
        {
            string result = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {

                //проверяем сущесвует ли пользователь
                bool checkIsExist = db.Article.Any(el => el.ArticleTitle == title);
                if (!checkIsExist)
                {
                    Article newarticle = new Article { ArticleType = "Поступления", ArticleTitle = title, UserId = SaveUser.CurrentUser.User_Id };
                    db.Article.Add(newarticle);
                    db.SaveChanges();
                    result = "Сделано!";
                }
                return result;
            }
        }
        //создать пользователя
        public static string CreatePaymentArticle(string title)
        {
            string result = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {

                //проверяем сущесвует ли пользователь
                bool checkIsExist = db.Article.Any(el => el.ArticleTitle == title);
                if (!checkIsExist)
                {
                    Article newarticle = new Article { ArticleType = "Выплаты", ArticleTitle = title, UserId = SaveUser.CurrentUser.User_Id };
                    db.Article.Add(newarticle);
                    db.SaveChanges();
                    result = "Сделано!";
                }
                return result;
            }
        }

        //содать проект
        public static string CreateProject(string name, string comment, Nullable<DateTime> startproject ,User user)
        {
            string result = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                //проверяем сущесвует ли позиция
                bool checkIsExist = db.Projects.Any(el => el.Projects_Name == name);
                if (!checkIsExist)
                {
                    Projects newProject = new Projects
                    {
                        Projects_Name = name,
                        Projects_StartTime = startproject,
                        Projects_Comment = comment,
                        UserId = user.User_Id
                    };
                    db.Projects.Add(newProject);
                    db.SaveChanges();
                    result = "Сделано!";
                }
                return result;
            }
        }
        //создать выплату
        public static string CreatePayment(DateTime? payment_Day, Account paymentAccount_Id, decimal? Payment_Sum, string Payment_article, Projects paymentProjects_Id, Сounterparty PaymentСounterparty_Id, string PaymentPurpose,User user)
        {
            string result = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                //check the user is exist
                bool checkIsExist = db.Payment.Any(el => el.Payment_Day == payment_Day  && el.PaymentAccount_Id == paymentAccount_Id && el.Payment_Sum == Payment_Sum && el.Payment_article == Payment_article && el.PaymentProjects_Id == paymentProjects_Id);
                if (!checkIsExist)
                {
                    Payment newPayment = new Payment
                    {
                        Payment_Day = payment_Day,
                        Account_Id = paymentAccount_Id.Account_Id,
                        Payment_Sum = Payment_Sum,
                        Payment_article = Payment_article,
                        Projects_Id = paymentProjects_Id.ProjectsId,
                        Counterparty_Id = PaymentСounterparty_Id.Сounterparty_Id,
                        PaymentPurpose = PaymentPurpose,
                        UserId = user.User_Id
                    };
                    Account account2 = db.Account.FirstOrDefault(p => p.Account_Id == paymentAccount_Id.Account_Id);
                    if (account2 != null)
                    {
                        account2.CurrentBalance -= newPayment.Payment_Sum;
                    }
                    db.Payment.Add(newPayment);
                    db.SaveChanges();
                    result = "Сделано!";
                }
                return result;
            }
        }

        // создание поступлений
        public static string CreatEntrance(DateTime? entrance_Day, Account entranceAccount_Id, decimal? entrance_Sum, string entrance_article, Projects entranceProjects_Id, Сounterparty entranceСounterparty_Id, string entrancePurpose, User user)
        {
            string result = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                //check the user is exist
                bool checkIsExist = db.Entrance.Any(el => el.Entrance_Day == entrance_Day && el.EntranceAccount_Id == entranceAccount_Id && el.Entrance_Sum == entrance_Sum && el.EntranceProjects_Id == entranceProjects_Id && el.EntranceСounterparty_Id == entranceСounterparty_Id && el.EntrancePurpose == entrancePurpose);
                if (!checkIsExist)
                {
                    Entrance newEntrance = new Entrance
                    {
                        Entrance_Day = entrance_Day,

                        Account_Id = entranceAccount_Id.Account_Id,
                        Entrance_Sum = entrance_Sum,
                        Entrance_article = entrance_article,
                        Projects_Id = entranceProjects_Id.ProjectsId,
                        Counterparty_Id = entranceСounterparty_Id.Сounterparty_Id,
                        EntrancePurpose = entrancePurpose,
                        UserId = user.User_Id
                    };
                    Account account2 = db.Account.FirstOrDefault(p => p.Account_Id == entranceAccount_Id.Account_Id);
                    if (account2 != null)
                    {
                        account2.CurrentBalance += newEntrance.Entrance_Sum;
                    }
                    db.Entrance.Add(newEntrance);
                    db.SaveChanges();
                    result = "Сделано!";
                }
                return result;
            }
        }

        // создание юр лица
        public static string CreatLegalEntity(string legalEntity_Name, string legalEntity_FullName, string legalEntity_Comment,User user)
        {
            string result = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                //check the user is exist
                bool checkIsExist = db.LegalEntity.Any(el => el.LegalEntity_Name == legalEntity_Name && el.LegalEntity_FullName == legalEntity_FullName && el.LegalEntity_Comment == legalEntity_Comment);
                if (!checkIsExist)
                {
                    LegalEntity newLegalEntity = new LegalEntity
                    {
                        LegalEntity_Name = legalEntity_Name,
                        LegalEntity_FullName = legalEntity_FullName,
                        LegalEntity_Comment = legalEntity_Comment,
                        UserId = user.User_Id
                    };
                    db.LegalEntity.Add(newLegalEntity);
                    db.SaveChanges();
                    result = "Сделано!";
                }
                return result;
            }
        }

   

        // создание контрагента    
        public static string CreatConterparty(string counterparty_Name, string counterparty_Comment,User user)
        {
            string result = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                //check the user is exist
                bool checkIsExist = db.Сounterparty.Any(el => el.Сounterparty_Name == counterparty_Name && el.Сounterparty_Comment == counterparty_Comment);
                if (!checkIsExist)
                {
                    Сounterparty newConterparty = new Сounterparty
                    {
                        Сounterparty_Name = counterparty_Name,
                        Сounterparty_Comment = counterparty_Comment,
                        UserId = user.User_Id
                    };
                    db.Сounterparty.Add(newConterparty);
                    db.SaveChanges();
                    result = "Сделано!";
                }
                return result;
            }
        }

        // создание счета    
        public static string CreatAccount(string account_Name, LegalEntity legalEntity, string account_Type, decimal? initialBalance, string account_Comment,DateTime? accountimput, User user)
        {
            string result = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                //check the user is exist
                bool checkIsExist = db.Account.Any(el => el.Account_Name == account_Name && el.Legal == legalEntity && el.Account_Type == account_Type  && el.Account_Comment == account_Comment);
                if (!checkIsExist)
                {
                    Account newAccount = new Account
                    {
                        Account_Name = account_Name,
                        LegalEntity_IdE = legalEntity.LegalEntity_Id,
                        Account_Type = account_Type,
                        CurrentBalance = initialBalance,
                        Account_Comment = account_Comment,
                        AccountDateImput = accountimput,
                        UserId = user.User_Id
                    };
                    db.Account.Add(newAccount);
                    db.SaveChanges();
                    result = "Сделано!";
                }
                return result;
            }
        }
        // Создание сообщения
        public static string CreatMessages(string message_Type, string message_Title, int message_Cost, User user)
        {
            string result = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            { 
                    Messages newMessages = new Messages
                    {
                        Message_Type = message_Type,
                        Message_Title = message_Title,
                        Message_Cost = message_Cost,
                        UserId = user.User_Id
                    };
                    db.Messages.Add(newMessages);
                    db.SaveChanges();
                    result = "Сделано!";
                return result;
            }
        }

        
        #endregion

        #region  Удаление сущности
        //удаление пользователя
        //public static string DeleteUser(User user)
        //{
        //    string result = "Такого пользователя не существует";
        //    using (ApplicationContext db = new ApplicationContext())
        //    {
        //        db.Users.Remove(user);
        //        db.SaveChanges();
        //        result = "Сделано! Подьзователь " + user.Name + " удален";
        //    }
        //    return result;
        //}
        //удаление проекта
        //public static string DeleteProject(Projects project)
        //{
        //    string result = "Такого проекта не существует";
        //    using (ApplicationContext db = new ApplicationContext())
        //    {
        //        //check position is exist
        //        db.Projects.Remove(project);
        //        db.SaveChanges();
        //        result = "Сделано! Проект " + project.Projects_Name + " удален";
        //    }
        //    return result;
        //}
        //удаление выплаты 
        //public static string DeletePayment(Payment payment)
        //{
        //    string result = "Такой выплаты не существует";
        //    using (ApplicationContext db = new ApplicationContext())
        //    {
        //        db.Payment.Remove(payment);
        //        db.SaveChanges();
        //        result = "Сделано! Выплата " + payment.Payment_Id + " удалена";
        //    }
        //    return result;
        //}

        public static string DeleteUser(User user)
        {
            string result = "Такого юр лица не существует";
            try
            {

                using (ApplicationContext db = new ApplicationContext())
                {
                    List<Entrance> listofentrance = db.Entrance.Where(p => p.UserId == user.User_Id).ToList();
                    List<Payment> listofpayment = db.Payment.Where(p => p.UserId == user.User_Id).ToList();
                    List<Projects> listofprojects = db.Projects.Where(p => p.UserId == user.User_Id).ToList();
                    List<Account> listofaccount = db.Account.Where(p => p.UserId == user.User_Id).ToList();
                    List<LegalEntity> listoflegalentity = db.LegalEntity.Where(p => p.UserId == user.User_Id).ToList();
                    List<Сounterparty> listofcounterparty = db.Сounterparty.Where(p => p.UserId == user.User_Id).ToList();
                    foreach (var item in listofentrance)
                    {
                        db.Entrance.Remove(item);
                    }
                    foreach (var item in listofpayment)
                    {
                        db.Payment.Remove(item);
                    }
                    foreach (var item in listofprojects)
                    {
                        db.Projects.Remove(item);
                    }
                    foreach (var item in listofaccount)
                    {
                        db.Account.Remove(item);
                    }
                    foreach (var item in listoflegalentity)
                    {
                        db.LegalEntity.Remove(item);
                    }
                    foreach (var item in listofcounterparty)
                    {
                        db.Сounterparty.Remove(item);
                    }
                    db.Users.Remove(user);
                    db.SaveChanges();
                    result = "Сделано! Пользователь: " + user.Name + " удален";

                }
            }
            catch (Exception exception)
            { }
            return result;
        }

        public static string DeleteMessage(Messages messages)
        {
            string result = "Такого сообщения не существует";
            try
            {

                using (ApplicationContext db = new ApplicationContext())
                {

                    db.Messages.Remove(messages);
                    db.SaveChanges();
                    result = "Сделано! Сообщение " + messages.Message_Title + " удалено";

                }
            }
            catch (Exception exception)
            { }
            return result;
        }

        public static string DeleteLegalEntity(LegalEntity legalEntity)
        {
            string result = "Такого юр лица не существует";
            try
            {

                using (ApplicationContext db = new ApplicationContext())
                {
                   
                    db.LegalEntity.Remove(legalEntity);
                    db.SaveChanges();
                    result = "Сделано! Юр лицо " + legalEntity.LegalEntity_Name + " удалено";

                }
            }
            catch (Exception exception)
            { }
            return result;
        }

        public static string DeleteEntranceArticle(Article article)
        {
            string result = "Такого юр лица не существует";
            try
            {

                using (ApplicationContext db = new ApplicationContext())
                {

                    db.Article.Remove(article);
                    db.SaveChanges();
                    result = "Сделано! Статья: " + article.ArticleTitle + " удалено";

                }
            }
            catch (Exception exception)
            { }
            return result;
        }

        public static string DeletePaymentArticle(Article article)
        {
            string result = "Такого юр лица не существует";
            try
            {

                using (ApplicationContext db = new ApplicationContext())
                {

                    db.Article.Remove(article);
                    db.SaveChanges();
                    result = "Сделано! Статья: " + article.ArticleTitle + " удалено";

                }
            }
            catch (Exception exception)
            { }
            return result;
        }

        public static string DeleteAccount(Account account)
        {
            string result = "Такого юр лица не существует";
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    db.Account.Remove(account);
                    db.SaveChanges();
                    result = "Сделано! Счет: " + account.Account_Name + " удален";
                }
            }
            catch (Exception exception)
            { }
            return result;
        }

        public static string DeleteProject(Projects projects)
        {
            string result = "Такого юр лица не существует";
            try
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    List <Entrance> listofentrance = db.Entrance.Where(p => p.Projects_Id == projects.ProjectsId).ToList();
                    List <Payment> listofpayment = db.Payment.Where(p => p.Projects_Id == projects.ProjectsId).ToList();
                    db.Projects.Remove(projects);
                    foreach (var item in listofentrance)
                    {
                        db.Entrance.Remove(item);
                    }
                    foreach (var item in listofpayment)
                    {
                        db.Payment.Remove(item);
                    }
                    db.SaveChanges();
                    result = "Сделано! Проект: " + projects.Projects_Name + " удален";
                }
        }
            catch (Exception exception)
            { }
            return result;
        }

        public static string DeleteCounterparty(Сounterparty сounterparty)
        {
            string result = "Такого юр лица не существует";
            try
            {

                using (ApplicationContext db = new ApplicationContext())
                {
                    db.Сounterparty.Remove(сounterparty);
                    db.SaveChanges();
                    result = "Сделано! Контрагент: " + сounterparty.Сounterparty_Name + " удален";

                }
            }
            catch (Exception exception)
            { }
            return result;
        }

        public static string DeleteEntrance(Entrance entrance)
        {
            string result = "Такого поступления не существует";
            try
            {

                using (ApplicationContext db = new ApplicationContext())
                {
                    db.Entrance.Remove(entrance);
                    db.SaveChanges();
                    result = "Сделано! Поступление: " + entrance.Entrance_article + " удален";

                }
            }
            catch (Exception exception)
            { }
            return result;
        }

        public static string DeletePayment(Payment payment)
        {
            string result = "Такого поступления не существует";
            try
            {

                using (ApplicationContext db = new ApplicationContext())
                {
                    db.Payment.Remove(payment);
                    db.SaveChanges();
                    result = "Сделано! Выплата: " + payment.PaymentPurpose + " удален";

                }
            }
            catch (Exception exception)
            { }
            return result;
        }


        //public static string DeleteEntrance(Entrance entrance)
        //{
        //    string result = "Такого поступления не существует";
        //    using (ApplicationContext db = new ApplicationContext())
        //    {
        //        db.Entrance.Remove(entrance);
        //        db.SaveChanges();
        //        result = "Сделано! Поступление " + entrance.Entrance_Id + " удалено";
        //    }
        //    return result;
        //}

        //public static string DeleteCounterparty(Сounterparty conterparty)
        //{
        //    string result = "Такого контрагента не существует";
        //    using (ApplicationContext db = new ApplicationContext())
        //    {
        //        db.Сounterparty.Remove(conterparty);
        //        db.SaveChanges();
        //        result = "Сделано! Контрагент " + conterparty.Сounterparty_Name + " удален";
        //    }
        //    return result;
        //}

        //public static string DeleteAccount(Account account)
        //{
        //    string result = "Такого счета не существует";
        //    using (ApplicationContext db = new ApplicationContext())
        //    {
        //        db.Account.Remove(account);
        //        db.SaveChanges();
        //        result = "Сделано! Счет " + account.Account_Name + " удален";
        //    }
        //    return result;
        //}

        #endregion

        #region Редактирование объектов 


        //редактирование  пользователя
        public static string EditUser(User oldUser, string newName, string newPassword, string newEmail)
        {
            string result = "Такого отела не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                User user = db.Users.FirstOrDefault(u => u.User_Id == oldUser.User_Id);
                user.Name = newName;
                user.Password = GetHash(newPassword);
                user.Email = newEmail;
               
             
               
                db.SaveChanges();
                result = "Сделано! Пользователь " + user.Name + " изменен";
            }
            return result;
        }


        //редактирование проекта
        public static string EditProject(Projects oldProject, string newProjects_Name, string newProjects_Comment, DateTime? projectstart)
        {
            string result = "Такого проекта не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                Projects project = db.Projects.FirstOrDefault(p => p.ProjectsId == oldProject.ProjectsId);
                project.Projects_Name = newProjects_Name;
                project.Projects_Comment = newProjects_Comment;
                project.Projects_StartTime = projectstart;
                db.SaveChanges();
                result = "Сделано! Проект " + project.Projects_Name + " изменен";
            }
            return result;
        }
        //редактирование выплаты
        public static string EditPayment(Payment oldPayment, DateTime? newPayment_Day, Account newPaymentAccount_Id, decimal? newPayment_Sum, Article newPayment_article, Projects newPaymentProjects_Id, Сounterparty newPaymentСounterparty_Id, string newPaymentPurpose)
        {
            string result = "Такой выплаты не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                //check user is exist
                Payment payment = db.Payment.FirstOrDefault(p => p.Payment_Id == oldPayment.Payment_Id);
                if (payment != null)
                {
                    payment.Payment_Day = newPayment_Day;
                    
                    payment.Account_Id = newPaymentAccount_Id.Account_Id;
                    payment.Payment_Sum = newPayment_Sum;
                    payment.Payment_article = newPayment_article.ArticleTitle;
                    payment.Projects_Id = newPaymentProjects_Id.ProjectsId;
                    payment.Counterparty_Id = newPaymentСounterparty_Id.Сounterparty_Id;
                    payment.PaymentPurpose = newPaymentPurpose;
                    db.SaveChanges();
                    result = "Сделано! Выплата " + payment.Payment_Id + " изменен";
                }
            }
            return result;
        }

 

        //редактирование юр лица
        public static string EditLegalEntity(LegalEntity oldLegalEntity, string newLegalEntity_Name, string newLegalEntity_FullName, string newLegalEntity_Comment)
        {
            string result = "Такого юр лица не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                //check user is exist
                LegalEntity legalEntity = db.LegalEntity.FirstOrDefault(l => l.LegalEntity_Id == oldLegalEntity.LegalEntity_Id);
                if (legalEntity != null)
                {
                    legalEntity.LegalEntity_Name = newLegalEntity_Name;
                    legalEntity.LegalEntity_FullName = newLegalEntity_FullName;
                    legalEntity.LegalEntity_Comment = newLegalEntity_Comment;
                    db.SaveChanges();
                    result = "Сделано! Юр лицо " + legalEntity.LegalEntity_Id + " изменено";
                }
            }
            return result;
        }

        //редактирование поступление
        public static string EditEntrance(Entrance oldEntrance, DateTime? newEntrance_Day, Account newEntranceAccount_Id, decimal? newEntrance_Sum, Article newEntrance_article, Projects newEntranceProjects_Id, Сounterparty newEntranceСounterparty_Id, string newEntrancePurpose)
        {
            string result = "Такого поступления не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                //check user is exist
                Entrance entrance = db.Entrance.FirstOrDefault(e => e.Entrance_Id == oldEntrance.Entrance_Id);
                if (entrance != null)
                {
                    entrance.Entrance_Day = newEntrance_Day;
                    
                    entrance.Account_Id = newEntranceAccount_Id.Account_Id;
                    entrance.Entrance_Sum = newEntrance_Sum;
                    entrance.Entrance_article = newEntrance_article.ArticleTitle;
                    entrance.Projects_Id = newEntranceProjects_Id.ProjectsId;
                    entrance.Counterparty_Id = newEntranceСounterparty_Id.Сounterparty_Id;
                    entrance.EntrancePurpose = newEntrancePurpose;

                    db.SaveChanges();
                    result = "Сделано! Поступление " + entrance.Entrance_Id + " изменено";
                }
            }
            return result;
        }

        //редактирование контрагента
        public static string EditConterparty(Сounterparty oldConterparty, string newСounterparty_Name, string newСounterparty_Comment)
        {
            string result = "Такого контрагента не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                //check user is exist
                Сounterparty сonterparty = db.Сounterparty.FirstOrDefault(c => c.Сounterparty_Id == oldConterparty.Сounterparty_Id);
                if (сonterparty != null)
                {
                    сonterparty.Сounterparty_Name = newСounterparty_Name;
                    сonterparty.Сounterparty_Comment = newСounterparty_Comment;

                    db.SaveChanges();
                    result = "Сделано! Контрагент " + сonterparty.Сounterparty_Id + " изменено";
                }
            }
            return result;
        }


        //редактирование счета
        public static string EditAccount(Account oldAccount, string newAccount_Name, LegalEntity newLegalEntity, string newAccount_Type, decimal? neweInitialBalance, string newAccount_Comment)
        {
            string result = "Такого счета не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                //check user is exist
                Account account = db.Account.FirstOrDefault(a => a.Account_Id == oldAccount.Account_Id);
                if (account != null)
                {
                    account.Account_Name = newAccount_Name;
                    account.LegalEntity_IdE = newLegalEntity.LegalEntity_Id;
                    account.Account_Type = newAccount_Type;
                    account.CurrentBalance = neweInitialBalance;

                    db.SaveChanges();
                    result = "Сделано! Счет " + account.Account_Id + " изменен";
                }
            }
            return result;
        }
        #endregion

        #region Получение позиции

        //получение позиции по id позитиции
        public static User GetPositionById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                User pos = db.Users.FirstOrDefault(p => p.User_Id == id);
                return pos;
            }
        }

     

        //получение отдела по id отдела
        public static LegalEntity GetLegalEntityById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                LegalEntity pos = db.LegalEntity.FirstOrDefault(p => p.LegalEntity_Id == id);
                return pos;
            }
        }

        //получение счета по id счета
        public static Account GetAccountById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Account pos = db.Account.FirstOrDefault(p => p.Account_Id == id);
                return pos;
            }
        }

        //получение проекта по id проекта
        public static Projects GetProjectsById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Projects pos = db.Projects.FirstOrDefault(p => p.ProjectsId == id);
                return pos;
            }
        }

        //получение контрагента по id контрагента
        public static Сounterparty GetCounterpartyById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Сounterparty pos = db.Сounterparty.FirstOrDefault(p => p.Сounterparty_Id == id);
                return pos;
            }
        }


        // Список счетов по айди
        public static ObservableCollection<Account> GetAllAccountById()
        {
            using (ApplicationContext db = new ApplicationContext())
            {

                List<Account> positions = db.Account.Where(p => p.UserId == SaveId).ToList();
                ObservableCollection<Account> position = new ObservableCollection<Account>(positions);
                return position;
            }
        }
        // Список юристов по айди
        public static ObservableCollection<LegalEntity> GetAllLegalEntityById()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
         
                List< LegalEntity> positions = db.LegalEntity.Where(p => p.UserId == SaveId).ToList();
                ObservableCollection<LegalEntity> position = new ObservableCollection<LegalEntity>(positions);
                return position;
            }
        }

        // Список проектов по айди 
        public static ObservableCollection<Projects> GetAllProjectsById()
        {
            using (ApplicationContext db = new ApplicationContext())
            {

                List<Projects> positions = db.Projects.Where(p => p.UserId == SaveId).ToList();
                ObservableCollection<Projects> position = new ObservableCollection<Projects>(positions);
                return position;
            }
        }

        // Список поступлений по айди 
        public static ObservableCollection<Entrance> GetAllEntranceById()
        {
            using (ApplicationContext db = new ApplicationContext())
            {

                List<Entrance> positions = db.Entrance.Where(p => p.UserId == SaveId).ToList();
                ObservableCollection<Entrance> position = new ObservableCollection<Entrance>(positions);
                return position;
            }
        }

        public static ObservableCollection<Entrance> GetAllEntranceByIdwithProject(Projects project)
        {
            using (ApplicationContext db = new ApplicationContext())
            {

                List<Entrance> positions = db.Entrance.Where(p => p.UserId == SaveId).Where(p =>  p.Projects_Id == project.ProjectsId).ToList();
                ObservableCollection<Entrance> position = new ObservableCollection<Entrance>(positions);
                return position;
            }
        }

        // Список поступлений по айди 
        public static ObservableCollection<Payment> GetAllPaymentById()
        {
            using (ApplicationContext db = new ApplicationContext())
            {

                List<Payment> positions = db.Payment.Where(p => p.UserId == SaveId).ToList();
                ObservableCollection<Payment> position = new ObservableCollection<Payment>(positions);
                return position;
            }
        }

        public static void AddEntranceArticles()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Article article1 = new Article
                {
                    ArticleType = "Поступления",
                    ArticleTitle = "Продажа товара",
                    UserId = SaveUser.CurrentUser.User_Id
                };

                Article article2 = new Article
                {
                    ArticleType = "Поступления",
                    ArticleTitle = "Оказание услуг",
                    UserId = SaveUser.CurrentUser.User_Id
                };

                Article article3 = new Article
                {
                    ArticleType = "Поступления",
                    ArticleTitle = "Процент по вкладам",
                    UserId = SaveUser.CurrentUser.User_Id
                };

                Article article4 = new Article
                {
                    ArticleType = "Поступления",
                    ArticleTitle = "Оборудование",
                    UserId = SaveUser.CurrentUser.User_Id
                };
                Article article5 = new Article
                {
                    ArticleType = "Поступления",
                    ArticleTitle = "Транспорт",
                    UserId = SaveUser.CurrentUser.User_Id
                };
                Article article6 = new Article
                {
                    ArticleType = "Поступления",
                    ArticleTitle = "Кредиты",
                    UserId = SaveUser.CurrentUser.User_Id
                }; Article article7 = new Article
                {
                    ArticleType = "Поступления",
                    ArticleTitle = "Вложения учредителей",
                    UserId = SaveUser.CurrentUser.User_Id
                };
                Article article8 = new Article
                {
                    ArticleType = "Поступления",
                    ArticleTitle = "Нематериальные активы",
                    UserId = SaveUser.CurrentUser.User_Id
                };
                Article article9 = new Article
                {
                    ArticleType = "Поступления",
                    ArticleTitle = "Финансовые вложения",
                    UserId = SaveUser.CurrentUser.User_Id
                };
                Article article10 = new Article
                {
                    ArticleType = "Поступления",
                    ArticleTitle = "Прочие доходы",
                    UserId = SaveUser.CurrentUser.User_Id
                };
                
                List<Article> articles = db.Article.Where(p => p.UserId == SaveId).Where(p => p.ArticleType == "Поступления").ToList();
                if (articles.Count == 0)
                {
                    db.Article.Add(article1);
                    db.Article.Add(article2);
                    db.Article.Add(article3);
                    db.Article.Add(article4);
                    db.Article.Add(article5);
                    db.Article.Add(article6);
                    db.Article.Add(article7);
                    db.Article.Add(article8);
                    db.Article.Add(article9);
                    db.Article.Add(article10);
                }
                db.SaveChanges();
 
            }
        }

        public static void AddPaymentArticles()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Article article1 = new Article
                {
                    ArticleType = "Выплаты",
                    ArticleTitle = "Производственных персонал",
                    UserId = SaveUser.CurrentUser.User_Id
                };

                Article article2 = new Article
                {
                    ArticleType = "Выплаты",
                    ArticleTitle = "Покупка товара",
                    UserId = SaveUser.CurrentUser.User_Id
                };

                Article article3 = new Article
                {
                    ArticleType = "Выплаты",
                    ArticleTitle = "Административный персонал",
                    UserId = SaveUser.CurrentUser.User_Id
                };

                Article article4 = new Article
                {
                    ArticleType = "Выплаты",
                    ArticleTitle = "Оборудование",
                    UserId = SaveUser.CurrentUser.User_Id
                };
                Article article5 = new Article
                {
                    ArticleType = "Выплаты",
                    ArticleTitle = "Транспорт",
                    UserId = SaveUser.CurrentUser.User_Id
                };
                Article article6 = new Article
                {
                    ArticleType = "Выплаты",
                    ArticleTitle = "Кредиты",
                    UserId = SaveUser.CurrentUser.User_Id
                }; Article article7 = new Article
                {
                    ArticleType = "Выплаты",
                    ArticleTitle = "Аренда",
                    UserId = SaveUser.CurrentUser.User_Id
                };
                Article article8 = new Article
                {
                    ArticleType = "Выплаты",
                    ArticleTitle = "Банковские услуги",
                    UserId = SaveUser.CurrentUser.User_Id
                };
                Article article9 = new Article
                {
                    ArticleType = "Выплаты",
                    ArticleTitle = "Финансовые вложения",
                    UserId = SaveUser.CurrentUser.User_Id
                };
                Article article10 = new Article
                {
                    ArticleType = "Выплаты",
                    ArticleTitle = "Прочие расходы",
                    UserId = SaveUser.CurrentUser.User_Id
                };
               
                List<Article> articles = db.Article.Where(p => p.UserId == SaveId).Where(p => p.ArticleType == "Выплаты").ToList();
                if (articles.Count < 1)
                {
                    db.Article.Add(article1);
                    db.Article.Add(article2);
                    db.Article.Add(article3);
                    db.Article.Add(article4);
                    db.Article.Add(article5);
                    db.Article.Add(article6);
                    db.Article.Add(article7);
                    db.Article.Add(article8);
                    db.Article.Add(article9);
                    db.Article.Add(article10);
                }
                db.SaveChanges();

            }
        }

        public static ObservableCollection<Article> GetAllEntranceArticle()
        {
            using (ApplicationContext db = new ApplicationContext())
            {

                List<Article> positions = db.Article.Where(p => p.UserId == SaveId).Where(p=>p.ArticleType == "Поступления").ToList();
                ObservableCollection<Article> position = new ObservableCollection<Article>(positions);
                return position;
            }
        }
        public static ObservableCollection<Article> GetAllPaymentArticle()
        {
            using (ApplicationContext db = new ApplicationContext())
            {

                List<Article> positions = db.Article.Where(p => p.UserId == SaveId).Where(p => p.ArticleType == "Выплаты").ToList();
                ObservableCollection<Article> position = new ObservableCollection<Article>(positions);
                return position;
            }
        }


        public static ObservableCollection<Payment> GetAllPaymentByIdwithProjects(Projects projects)
        {
            using (ApplicationContext db = new ApplicationContext())
            {

                List<Payment> positions = db.Payment.Where(p => p.UserId == SaveId).Where(p => p.Projects_Id == projects.ProjectsId).ToList();
                ObservableCollection<Payment> position = new ObservableCollection<Payment>(positions);
                return position;
            }
        }

        // Подсчет общего дохода 
        public static decimal? GetTotalEntranceById()
        {
            using (ApplicationContext db = new ApplicationContext())
            {

                List<Entrance> positions = db.Entrance.Where(p => p.UserId == SaveId).ToList();
                ObservableCollection<Entrance> position = new ObservableCollection<Entrance>(positions);
                decimal? income = 0;
              
                foreach (var el in position)
                {
                    if (el.Entrance_Sum > 0) income += el.Entrance_Sum;
                   
                }
              
                return income;
            }
        }
        // Подсчет общих выплат 
        public static decimal? GetTotalPaymentById()
        {
            using (ApplicationContext db = new ApplicationContext())
            {

                List<Payment> positions = db.Payment.Where(p => p.UserId == SaveId).ToList();
                ObservableCollection<Payment> position = new ObservableCollection<Payment>(positions);
                decimal? income = 0;

                foreach (var el in position)
                {
                    if (el.Payment_Sum > 0) income += el.Payment_Sum;

                }

                return income;
            }
        }

        public static decimal? GetRangeById()
        {
            using (ApplicationContext db = new ApplicationContext())
            {

                List<Payment> positions = db.Payment.Where(p => p.UserId == SaveId).ToList();
                ObservableCollection<Payment> position = new ObservableCollection<Payment>(positions);
                List<Entrance> positions2 = db.Entrance.Where(p => p.UserId == SaveId).ToList();
                ObservableCollection<Entrance> position2 = new ObservableCollection<Entrance>(positions2);
                decimal? income = 0;
                decimal? output = 0;
                decimal? total = 0;

                foreach (var el in position)
                {
                    if (el.Payment_Sum > 0) income += el.Payment_Sum;

                }
                foreach (var el in position2)
                {
                    if (el.Entrance_Sum > 0) output += el.Entrance_Sum;
                    total = output - income;
                }

                return total;
            }
        }
        public static decimal? GetDevedentsId()
        {
            using (ApplicationContext db = new ApplicationContext())
            {

                List<Payment> positions = db.Payment.Where(p => p.UserId == SaveId).ToList();
                ObservableCollection<Payment> position = new ObservableCollection<Payment>(positions);
                List<Entrance> positions2 = db.Entrance.Where(p => p.UserId == SaveId).ToList();
                ObservableCollection<Entrance> position2 = new ObservableCollection<Entrance>(positions2);
                decimal? income = 0;
                decimal? output = 0;
                decimal? total = 0;           
             
                foreach (var el in position)
                {
                    if (el.Payment_Sum > 0) income += el.Payment_Sum;

                }
                foreach (var el in position2)
                {
                    if (el.Entrance_Sum > 0) output += el.Entrance_Sum;
                    total = (output - income)*10/100;
                }

                return total;
            }
        }



        // Получение Имени пользователя
     
        public static ObservableCollection<User> GetFindUserNameById(string name)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
               
                List<User> positions = db.Users.Where(p => p.Name.StartsWith(name)).ToList();
                ObservableCollection<User> user = new ObservableCollection<User>(positions);
                return user;
            }
        }

        public static ObservableCollection<Messages> GetFindMessageUserNameById(string name)
        {
            using (ApplicationContext db = new ApplicationContext())
            {

                List<Messages> positions = db.Messages.Where(p => p.user.Name.StartsWith(name)).ToList();
                ObservableCollection<Messages> user = new ObservableCollection<Messages>(positions);
                return user;
            }
        }

        public static ObservableCollection<Messages> GetFindMessageUserEmailById(string name)
        {
            using (ApplicationContext db = new ApplicationContext())
            {

                List<Messages> positions = db.Messages.Where(p => p.user.Email.StartsWith(name)).ToList();
                ObservableCollection<Messages> user = new ObservableCollection<Messages>(positions);
                return user;
            }
        }

        public static ObservableCollection<User> GetFindUserEmailById(string email)
        {
            using (ApplicationContext db = new ApplicationContext())
            {

                List<User> positions = db.Users.Where(p => p.Email.StartsWith(email)).ToList();
                ObservableCollection<User> user = new ObservableCollection<User>(positions);
                return user;
            }
        }

        // Получение Имени пользователя
        private static string username;
        public static string GetFindUserNameById()
        {
            using (ApplicationContext db = new ApplicationContext())
            {

                User positions = db.Users.FirstOrDefault(p => p.User_Id == SaveId);
                username = positions.Name;
                return username;
            }
        }

        // Получение имейла пользователя
        private static string useremail;
        public static string GetFindUserEmailById()
        {
            using (ApplicationContext db = new ApplicationContext())
            {

                User positions = db.Users.FirstOrDefault(p => p.User_Id == SaveId);
               
                    useremail = positions.Email;
                
                return useremail;

            }
        }

        // Получение результата всех счетов пользователя
        private static decimal? userеtotalaccount;
        public static decimal? GetFindUserTotalAccountById()
        {
            using (ApplicationContext db = new ApplicationContext())
            {

                List<Account> accounts = db.Account.Where(p => p.Account_Id == SaveId).ToList();

                foreach (var item in accounts)
                {
                    userеtotalaccount += item.CurrentBalance;
                }

                return userеtotalaccount;

            }
        }

        // Поиск юрлиц по названию
        public static ObservableCollection<LegalEntity> GetFindLegalEntityById(string LegalEntityName)
        {
            using (ApplicationContext db = new ApplicationContext())
            {

                List<LegalEntity> positions = db.LegalEntity.Where(p => p.UserId == SaveId).Where(p => p.LegalEntity_Name.StartsWith(LegalEntityName)).ToList();
                ObservableCollection<LegalEntity> position = new ObservableCollection<LegalEntity>(positions);
                return position;
            }
        }

       
       
        // Поиск счетов по названию
        public static ObservableCollection<Account> GetFindAccountById(string AccountName)
        {
            using (ApplicationContext db = new ApplicationContext())
            {

                List<Account> positions = db.Account.Where(p => p.UserId == SaveId).Where(p => p.Account_Name.StartsWith(AccountName)).ToList();
                ObservableCollection<Account> position = new ObservableCollection<Account>(positions);
                return position;
            }
        }

        // Поиск счетов по типу 
        public static ObservableCollection<Account> GetFindAccountByNTypeId(string NType)
        {
            using (ApplicationContext db = new ApplicationContext())
            {

                List<Account> positions = db.Account.Where(p => p.UserId == SaveId).Where(p => p.Account_Type == NType).ToList();
                ObservableCollection<Account> position = new ObservableCollection<Account>(positions);
                return position;
            }
        }

        public static ObservableCollection<Messages> GetFindMesssagesByFirstTypeId(string NType)
        {
            using (ApplicationContext db = new ApplicationContext())
            {

                List<Messages> positions = db.Messages.Where(p => p.Message_Type == NType).ToList();
                ObservableCollection<Messages> position = new ObservableCollection<Messages>(positions);
                return position;
            }
        }

        // Поиск контагентов по названию
        public static ObservableCollection<Сounterparty> GetFindCounterPartyById(string counterPartytName)
        {
            using (ApplicationContext db = new ApplicationContext())
            {

                List<Сounterparty> positions = db.Сounterparty.Where(p => p.UserId == SaveId).Where(p => p.Сounterparty_Name.StartsWith(counterPartytName)).ToList();
                ObservableCollection<Сounterparty> position = new ObservableCollection<Сounterparty>(positions);
                return position;
            }
        }

        // Поиск пользователя  по id
        public static ObservableCollection<User> GetFindUserById(string username)
        {
            using (ApplicationContext db = new ApplicationContext())
            {

                List<User> positions = db.Users.Where(p => p.Name.StartsWith(username)).ToList();
                ObservableCollection<User> position = new ObservableCollection<User>(positions);
                return position;
            }
        }
        // Поиск поступления по типу по названию
        public static ObservableCollection<Entrance> GetFindEntranceById(string artical)
        {
            using (ApplicationContext db = new ApplicationContext())
            {

                List<Entrance> positions = db.Entrance.Where(p => p.UserId == SaveId).Where(p => p.Entrance_article.StartsWith(artical)).ToList();
                ObservableCollection<Entrance> position = new ObservableCollection<Entrance>(positions);
                return position;
            }
        }
        // Поиск выплаты по типу 
        public static ObservableCollection<Payment> GetFindPaymentById(string artical)
        {
            using (ApplicationContext db = new ApplicationContext())
            {

                List<Payment> positions = db.Payment.Where(p => p.UserId == SaveId).Where(p => p.Payment_article.StartsWith(artical)).ToList();
                ObservableCollection<Payment> position = new ObservableCollection<Payment>(positions);
                return position;
            }
        }
       
        // Поиск проекта по названию  по названию
        public static ObservableCollection<Projects> GetFindProjectById(string projectName)
        {
            using (ApplicationContext db = new ApplicationContext())
            {

                List<Projects> positions = db.Projects.Where(p => p.UserId == SaveId).Where(p => p.Projects_Name.StartsWith(projectName)).ToList();
                ObservableCollection<Projects> position = new ObservableCollection<Projects>(positions);
                return position;
            }
        }

        // Поиск проекта по дате начала
        public static ObservableCollection<Projects> GetFindProjectByDateId(DateTime projecttime)
        {
            using (ApplicationContext db = new ApplicationContext())
            {

                List<Projects> positions = db.Projects.Where(p => p.UserId == SaveId).Where(p => p.Projects_StartTime.Value == projecttime.Date).ToList();
                ObservableCollection<Projects> position = new ObservableCollection<Projects>(positions);
                return position;
            }
        }

        // Поиск поступления по дате 
        public static ObservableCollection<Entrance> GetFindEntranceByDateId(DateTime entrancetime)
        {
            using (ApplicationContext db = new ApplicationContext())
            {

                List<Entrance> positions = db.Entrance.Where(p => p.UserId == SaveId).Where(p => p.Entrance_Day.Value == entrancetime.Date).ToList();
                ObservableCollection<Entrance> position = new ObservableCollection<Entrance>(positions);
                return position;
            }
        }

        // Поиск выплаты по дате
        public static ObservableCollection<Payment> GetFindPaymentByDateId(DateTime paymenttime)
        {
            using (ApplicationContext db = new ApplicationContext())
            {

                List<Payment> positions = db.Payment.Where(p => p.UserId == SaveId).Where(p => p.Payment_Day.Value == paymenttime.Date).ToList();
                ObservableCollection<Payment> position = new ObservableCollection<Payment>(positions);
                return position;
            }
        }

        public static ObservableCollection<Сounterparty> GetAllСounterpartyById()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
          
                List<Сounterparty> positions = db.Сounterparty.Where(p => p.UserId == SaveId).ToList();
                ObservableCollection<Сounterparty> position = new ObservableCollection<Сounterparty>(positions);
                return position;
            }
        }

  

        public static int SaveId { get; set; }
        public static bool AudintificateUser(string password, string email)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                bool check = false;
                
                var user = (from users in GetAllUsers() select users).Where(n=> n.Email == email && n.Password == GetHash(password)).ToList();
             
                if (user.Count > 0)
                {
                    foreach (User us in user)
                    {
                        SaveUser.CurrentUser = us;
                        SaveId = us.User_Id;
                    }
                    check = true;
                }
                else 
                {
                    check = false;
                }
           
                return check;
            }
        }
   
        #endregion
    }
}
