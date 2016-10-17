﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Data.Entity;
using RepoQuiz.Models;
using RepoQuiz.DAL;

namespace RepoQuiz.Tests.DAL
{
    [TestClass]
    public class StudentRepositoryTests
    {
        Mock<StudentContext> mock_context { get; set; }

        Mock<DbSet<FirstNamePick>> firstName_mock_dbset { get; set; }
        Mock<DbSet<LastNamePick>> lastName_mock_dbset { get; set; }
        Mock<DbSet<MajorPick>> major_mock_dbset { get; set; }

        List<FirstNamePick> firstName_datastore { get; set; }
        List<LastNamePick> lastName_datastore { get; set; }
        List<MajorPick> major_datastore { get; set; }

        StudentRepository repo { get; set; }



        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<StudentContext>();

            firstName_mock_dbset = new Mock<DbSet<FirstNamePick>>();
            lastName_mock_dbset = new Mock<DbSet<LastNamePick>>();
            major_mock_dbset = new Mock<DbSet<MajorPick>>();

            firstName_datastore = new List<FirstNamePick>();//fake database
            lastName_datastore = new List<LastNamePick>();
            major_datastore = new List<MajorPick>();

            repo = new StudentRepository(mock_context.Object);

            var firstName_queryable_list = firstName_datastore.AsQueryable();
            var lastName_queryable_list = lastName_datastore.AsQueryable();
            var major_queryable_list = major_datastore.AsQueryable();
            
            firstName_mock_dbset.As<IQueryable<FirstNamePick>>().Setup(m => m.Provider).Returns(firstName_queryable_list.Provider);//where data from
            firstName_mock_dbset.As<IQueryable<FirstNamePick>>().Setup(m => m.Expression).Returns(firstName_queryable_list.Expression);//e.g. SQL query is an expression; a big expression could be seperate into two expressions
            firstName_mock_dbset.As<IQueryable<FirstNamePick>>().Setup(m => m.ElementType).Returns(firstName_queryable_list.ElementType);//key words is a element type, e.g. SELECT, FROM; * simbal; table, 3 element type
            firstName_mock_dbset.As<IQueryable<FirstNamePick>>().Setup(m => m.GetEnumerator()).Returns(() => firstName_queryable_list.GetEnumerator());//could loop over ordered

            lastName_mock_dbset.As<IQueryable<LastNamePick>>().Setup(m => m.Provider).Returns(lastName_queryable_list.Provider);//where data from
            lastName_mock_dbset.As<IQueryable<LastNamePick>>().Setup(m => m.Expression).Returns(lastName_queryable_list.Expression);//e.g. SQL query is an expression; a big expression could be seperate into two expressions
            lastName_mock_dbset.As<IQueryable<LastNamePick>>().Setup(m => m.ElementType).Returns(lastName_queryable_list.ElementType);//key words is a element type, e.g. SELECT, FROM; * simbal; table, 3 element type
            lastName_mock_dbset.As<IQueryable<LastNamePick>>().Setup(m => m.GetEnumerator()).Returns(() => lastName_queryable_list.GetEnumerator());

            major_mock_dbset.As<IQueryable<MajorPick>>().Setup(m => m.Provider).Returns(major_queryable_list.Provider);//where data from
            major_mock_dbset.As<IQueryable<MajorPick>>().Setup(m => m.Expression).Returns(major_queryable_list.Expression);//e.g. SQL query is an expression; a big expression could be seperate into two expressions
            major_mock_dbset.As<IQueryable<MajorPick>>().Setup(m => m.ElementType).Returns(major_queryable_list.ElementType);//key words is a element type, e.g. SELECT, FROM; * simbal; table, 3 element type
            major_mock_dbset.As<IQueryable<MajorPick>>().Setup(m => m.GetEnumerator()).Returns(() => major_queryable_list.GetEnumerator());

            //mock context return the mock_variable_table when someone calls the SavingVariableContext.charValueDb
            mock_context.Setup(c => c.firstNamePick).Returns(firstName_mock_dbset.Object);
            mock_context.Setup(c => c.lastNamePick).Returns(lastName_mock_dbset.Object);
            mock_context.Setup(c => c.majorPick).Returns(major_mock_dbset.Object);

            //capture when use Add function, instead use variable_datastore
            firstName_mock_dbset.Setup(t => t.Add(It.IsAny<FirstNamePick>())).Callback((FirstNamePick a/*capture the variable sent*/) => firstName_datastore.Add(a)/*add it to a list*/);
            lastName_mock_dbset.Setup(t => t.Add(It.IsAny<LastNamePick>())).Callback((LastNamePick a/*capture the variable sent*/) => lastName_datastore.Add(a)/*add it to a list*/);
            major_mock_dbset.Setup(t => t.Add(It.IsAny<MajorPick>())).Callback((MajorPick a/*capture the variable sent*/) => major_datastore.Add(a)/*add it to a list*/);

            firstName_mock_dbset.Setup(t => t.Remove(It.IsAny<FirstNamePick>())).Callback((FirstNamePick a) => firstName_datastore.Remove(a));
            lastName_mock_dbset.Setup(t => t.Remove(It.IsAny<LastNamePick>())).Callback((LastNamePick a) => lastName_datastore.Remove(a));
            major_mock_dbset.Setup(t => t.Remove(It.IsAny<MajorPick>())).Callback((MajorPick a) => major_datastore.Remove(a));
        }

        [TestCleanup]
        public void ClearUp()
        {
            repo = null;
        }
        
    }
}
