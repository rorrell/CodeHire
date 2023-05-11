namespace CodeHire.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedJobListingsAndLanguages : DbMigration
    {
        public override void Up()
        {
            //Seed Languages
            Sql("INSERT INTO Languages (Name) VALUES ('C#')");
            Sql("INSERT INTO Languages (Name) VALUES ('Java')");
            Sql("INSERT INTO Languages (Name) VALUES ('Node')");
            Sql("INSERT INTO Languages (Name) VALUES ('JavaScript')");
            Sql("INSERT INTO Languages (Name) VALUES ('Ruby')");
            Sql("INSERT INTO Languages (Name) VALUES ('Angular')");
            Sql("INSERT INTO Languages (Name) VALUES ('SQL')");
            Sql("INSERT INTO Languages (Name) VALUES ('MongoDb')");
            Sql("INSERT INTO Languages (Name) VALUES ('Python')");

            //Seed JobListings
            Sql("Insert INTO JobListings (Title, Company, Location, Description, ExpirationDate, Wage) VALUES ('Software Engineer', 'Meta', 'San Francisco, CA', 'Become a software engineer with us today!  Must have 5 years of experience.  Need to have used Agile workflow.', NULL, '$100k-$150k/year')");
            Sql("Insert INTO JobListings (Title, Company, Location, Description, ExpirationDate, Wage) VALUES ('Data Engineer', 'Netflix', 'Fremont, CA', 'Date engineer needed urgently!  At least 4 years of experience.', '20230912 12:00:00 AM', NULL)");
            Sql("Insert INTO JobListings (Title, Company, Location, Description, ExpirationDate, Wage) VALUES ('QA Specialist', 'Forward', 'Oakland, CA', 'QA specialist with 3+ years of experience and higher degree needed.', '20230423 12:00:00 AM', NULL)");
            Sql("Insert INTO JobListings (Title, Company, Location, Description, ExpirationDate, Wage) VALUES ('Backend Engineer', 'Hertz', 'Durham, NC', 'Backend engineer with cloud specialty, CS degree, and 2+ years of experience.', '20230608 12:00:00 AM', '$58/hr')");
            Sql("Insert INTO JobListings (Title, Company, Location, Description, ExpirationDate, Wage) VALUES ('Frontend Engineer', 'DocuSign', 'San Jose, CA', 'Frontend engineer with mobile knowledge and 3+ years of experience requested.', '20231014 12:00:00 AM', '$98k-130k/year')");

            //Seed LanguageJobListings
            Sql("INSERT INTO LanguageJobListings (Language_Id, JobListing_Id) VALUES (1, 1)");
            Sql("INSERT INTO LanguageJobListings (Language_Id, JobListing_Id) VALUES (6, 1)");
            Sql("INSERT INTO LanguageJobListings (Language_Id, JobListing_Id) VALUES (7, 1)");
            Sql("INSERT INTO LanguageJobListings (Language_Id, JobListing_Id) VALUES (9, 2)");
            Sql("INSERT INTO LanguageJobListings (Language_Id, JobListing_Id) VALUES (3, 4)");
            Sql("INSERT INTO LanguageJobListings (Language_Id, JobListing_Id) VALUES (5, 4)");
            Sql("INSERT INTO LanguageJobListings (Language_Id, JobListing_Id) VALUES (3, 5)");
            Sql("INSERT INTO LanguageJobListings (Language_Id, JobListing_Id) VALUES (4, 5)");
            Sql("INSERT INTO LanguageJobListings (Language_Id, JobListing_Id) VALUES (8, 5)");
        }
        
        public override void Down()
        {
        }
    }
}
