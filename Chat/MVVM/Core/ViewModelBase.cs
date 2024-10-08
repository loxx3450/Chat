﻿using Chat.Core;
using Chat.MVVM.Models.Instances.Configs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.MVVM.Core
{
    public abstract class ViewModelBase : ObservableObject
    {
        public abstract IConfig? Config { get; }


        protected Dictionary<string, bool> propertyErrors = [];

        public bool HasErrors => propertyErrors.Count > 0;

        protected void ValidateProperty<T>(T value, string name)
        {
            try
            {
                propertyErrors.Remove(name);

                Validator.ValidateProperty(value, new ValidationContext(this, null, null)
                {
                    MemberName = name
                });
            }
            catch
            {
                propertyErrors.Add(name, true);

                throw;
            }
        }

        protected void SetValidatedField<T>(ref T property, T value, string propertyName)
        {
            SetField(ref property, value, propertyName);
            ValidateProperty(value, propertyName);
        }

        public abstract void ResetData();
    }
}
