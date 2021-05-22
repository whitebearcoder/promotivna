﻿using MarkomPos.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkomPos.Repository.Repository
{
    public class DocumentParityRepository : IDisposable
    {
        public bool AddUpdateDocumentParty(DocumentParity paymentMethod)
        {
            using (var context = new markomPosDbContext())
            {
                try
                {
                    if (paymentMethod.ID > 0)
                    {
                        var dbData = context.DocumentParities.Find(paymentMethod.ID);
                        if (dbData != null)
                        {
                            dbData.ID = paymentMethod.ID;
                            dbData.Name = paymentMethod.Name;
                            dbData.DisplayName = paymentMethod.DisplayName;
                            dbData.DateModified = DateTime.Now;
                        }
                    }
                    else
                    {
                        var isExist = context.DocumentParities.Any(f => f.Name == paymentMethod.Name);
                        if (!isExist)
                        {
                            paymentMethod.DateCreated = DateTime.Now;
                            paymentMethod.DateModified = DateTime.Now;
                            context.DocumentParities.Add(paymentMethod);
                        }
                    }
                    context.SaveChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        public void Dispose()
        {
        }
    }
}