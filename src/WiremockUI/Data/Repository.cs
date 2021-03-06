﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PocDatabase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace WiremockUI.Data
{
    public class Schema
    {
        // Change this property when the schema changes completely.
        // Example: Some important field is renamed
        private int _version = 1;

        public int Version
        {
            get => _version;
            set { }
        }

        public List<Server> Servers { get; set; }
        public List<Settings> Settings { get; set; }
    }

    public class UnitOfWork
    {
        public static PocFile<Schema> _staticPocFile;
        private readonly Dictionary<Type, object> repositories;

        public static PocFile<Schema> PocFile
        {
            get
            {
                if (_staticPocFile == null)
                    _staticPocFile = new PocFile<Schema>();
                return _staticPocFile;
            }
            set
            {
                _staticPocFile = value;
            }
        }

        public UnitOfWork()
        {
            this.repositories = new Dictionary<Type, object>();
        }
        
        public Repository<Server> Servers
        {
            get
            {
                Repository<Server> repository;
                if (repositories.ContainsKey(typeof(Server)))
                    repository = (Repository<Server>)repositories[typeof(Server)];
                else
                    repository = new Repository<Server>(PocFile);

                return repository;
            }
        }

        public Repository<Settings> Settings
        {
            get
            {
                Repository<Settings> repository;
                if (repositories.ContainsKey(typeof(Settings)))
                    repository = (Repository<Settings>)repositories[typeof(Settings)];
                else
                    repository = new Repository<Settings>(PocFile);

                return repository;
            }
        }

        public void Save()
        {
            lock (_staticPocFile)
                PocFile.Save();
        }

        public static void ClearCache()
        {
            PocFile = null;
        }

        public static int GetVersion(string path)
        {
            if (File.Exists(path))
            {
                // read JSON directly from a file
                using (StreamReader file = File.OpenText(path))
                {
                    using (JsonTextReader reader = new JsonTextReader(file))
                    {
                        var json = (JObject)JToken.ReadFrom(reader);
                        var version = json["Version"]?.Value<int>();
                        if (version != null)
                            return version.Value;
                    }
                }

            }

            return 0;
        }
    }

    public class Repository<T>
    {
        private PocFile<Schema> pocFile;
        private PocRepository<Schema, T> repository;
        
        public Repository(PocFile<Schema> pocFile)
        {
            this.pocFile = pocFile;
            this.repository = this.pocFile.GetRepository<T>();
        }

        public IQueryable<T> AsQueryable()
        {
            return this.repository.AsQueryable();
        }

        public void Delete(Guid id)
        {
            this.repository.Delete(id);
        }

        public void Delete(T entityToDelete)
        {
            this.repository.Delete(entityToDelete);
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            return this.repository.Get(filter, orderBy, includeProperties);
        }

        public IEnumerable<T> GetAll()
        {
            return this.repository.GetAll();
        }

        public T GetById(Guid id)
        {
            return this.repository.GetById(id);
        }

        public void Insert(T entity)
        {
            this.repository.Insert(entity);
        }

        public void Update(T entityToUpdate)
        {
            this.repository.Update(entityToUpdate);
        }
    }
}
