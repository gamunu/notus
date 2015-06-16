using System;
using System.Data.Entity.Migrations;
using System.Linq;
using Microsoft.Ajax.Utilities;
using Notus.Hub.Models;

namespace Notus.Hub.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            var countries = new[]
            {
                new Country {Name = "Brazil", Code = "BR"},
                new Country {Name = "British Indian Ocean Territory", Code = "IO"},
                new Country {Name = "Brunei", Code = "BN"},
                new Country {Name = "Bulgaria", Code = "BG"},
                new Country {Name = "Burkina Faso", Code = "BF"},
                new Country {Name = "Djibouti", Code = "DJ"},
                new Country {Name = "Dominica", Code = "DM"},
                new Country {Name = "Dominican Republic", Code = "DO"},
                new Country {Name = "Ecuador", Code = "EC"},
                new Country {Name = "Egypt", Code = "EG"},
                new Country {Name = "El Salvador", Code = "SV"},
                new Country {Name = "Equatorial Guinea", Code = "GQ"},
                new Country {Name = "Eritrea", Code = "ER"},
                new Country {Name = "Estonia", Code = "EE"},
                new Country {Name = "Ethiopia", Code = "ET"},
                new Country {Name = "Falkland Islands", Code = "FK"},
                new Country {Name = "Faroe Islands", Code = "FO"},
                new Country {Name = "Fiji Islands", Code = "FJ"},
                new Country {Name = "Finland", Code = "FI"},
                new Country {Name = "France", Code = "FR"},
                new Country {Name = "French Guiana", Code = "GF"},
                new Country {Name = "French Polynesia", Code = "PF"},
                new Country {Name = "French Southern and Antarctic Lands", Code = "TF"},
                new Country {Name = "Gabon", Code = "GA"},
                new Country {Name = "Gambia, The", Code = "GM"},
                new Country {Name = "Georgia", Code = "GE"},
                new Country {Name = "Germany", Code = "DE"},
                new Country {Name = "Ghana", Code = "GH"},
                new Country {Name = "Gibraltar", Code = "GI"},
                new Country {Name = "Greece", Code = "GR"},
                new Country {Name = "Greenland", Code = "GL"},
                new Country {Name = "Grenada", Code = "GD"},
                new Country {Name = "Guadeloupe", Code = "GP"},
                new Country {Name = "Guam", Code = "GU"},
                new Country {Name = "Guatemala", Code = "GT"},
                new Country {Name = "Guernsey", Code = "GG"},
                new Country {Name = "Guinea", Code = "GN"},
                new Country {Name = "Guinea-Bissau", Code = "GW"},
                new Country {Name = "Guyana", Code = "GY"},
                new Country {Name = "Haiti", Code = "HT"},
                new Country {Name = "Heard Island and McDonald Islands", Code = "HM"},
                new Country {Name = "Holy See (Vatican City)", Code = "VA"},
                new Country {Name = "Honduras", Code = "HN"},
                new Country {Name = "Hong Kong SAR", Code = "HK"},
                new Country {Name = "Hungary", Code = "HU"},
                new Country {Name = "Iceland", Code = "IS"},
                new Country {Name = "India", Code = "IN"},
                new Country {Name = "Indonesia", Code = "ID"},
                new Country {Name = "Iran", Code = "IR"},
                new Country {Name = "Iraq", Code = "IQ"},
                new Country {Name = "Ireland", Code = "IE"},
                new Country {Name = "Isle of Man", Code = "IM"},
                new Country {Name = "Israel", Code = "IL"},
                new Country {Name = "Italy", Code = "IT"},
                new Country {Name = "Jamaica", Code = "JM"},
                new Country {Name = "Jan Mayen", Code = "SJ"},
                new Country {Name = "Japan", Code = "JP"},
                new Country {Name = "Jersey", Code = "JE"},
                new Country {Name = "Jordan", Code = "JO"},
                new Country {Name = "Kazakhstan", Code = "KZ"},
                new Country {Name = "Kenya", Code = "KE"},
                new Country {Name = "Kiribati", Code = "KI"},
                new Country {Name = "Korea", Code = "KR"},
                new Country {Name = "Kosovo", Code = "XK"},
                new Country {Name = "Kuwait", Code = "KW"},
                new Country {Name = "Kyrgyzstan", Code = "KG"},
                new Country {Name = "Laos", Code = "LA"},
                new Country {Name = "Latvia", Code = "LV"},
                new Country {Name = "Lebanon", Code = "LB"},
                new Country {Name = "Lesotho", Code = "LS"},
                new Country {Name = "Liberia", Code = "LR"},
                new Country {Name = "Libya", Code = "LY"},
                new Country {Name = "Liechtenstein", Code = "LI"},
                new Country {Name = "Lithuania", Code = "LT"},
                new Country {Name = "Luxembourg", Code = "LU"},
                new Country {Name = "Macao SAR", Code = "MO"},
                new Country {Name = "Macedonia, Former Yugoslav Republic of", Code = "MK"},
                new Country {Name = "Madagascar", Code = "MG"},
                new Country {Name = "Malawi", Code = "MW"},
                new Country {Name = "Malaysia", Code = "MY"},
                new Country {Name = "Maldives", Code = "MV"},
                new Country {Name = "Mali", Code = "ML"},
                new Country {Name = "Malta", Code = "MT"},
                new Country {Name = "Marshall Islands", Code = "MH"},
                new Country {Name = "Martinique", Code = "MQ"},
                new Country {Name = "Mauritania", Code = "MR"},
                new Country {Name = "Mauritius", Code = "MU"},
                new Country {Name = "Mayotte", Code = "YT"},
                new Country {Name = "Mexico", Code = "MX"},
                new Country {Name = "Micronesia", Code = "FM"},
                new Country {Name = "Moldova", Code = "MD"},
                new Country {Name = "Monaco", Code = "MC"},
                new Country {Name = "Mongolia", Code = "MN"},
                new Country {Name = "Montenegro", Code = "ME"},
                new Country {Name = "Montserrat", Code = "MS"},
                new Country {Name = "Morocco", Code = "MA"},
                new Country {Name = "Mozambique", Code = "MZ"},
                new Country {Name = "Myanmar", Code = "MM"},
                new Country {Name = "Namibia", Code = "NA"},
                new Country {Name = "Nauru", Code = "NR"},
                new Country {Name = "Nepal", Code = "NP"},
                new Country {Name = "Netherlands", Code = "NL"},
                new Country {Name = "Netherlands Antilles (Former)", Code = "AN"},
                new Country {Name = "Caledonia", Code = "NC"},
                new Country {Name = "Zealand", Code = "NZ"},
                new Country {Name = "Nicaragua", Code = "NI"},
                new Country {Name = "Niger", Code = "NE"},
                new Country {Name = "Nigeria", Code = "NG"},
                new Country {Name = "Niue", Code = "NU"},
                new Country {Name = "Norfolk Island", Code = "NF"},
                new Country {Name = "North Korea", Code = "KP"},
                new Country {Name = "Northern Mariana Islands", Code = "MP"},
                new Country {Name = "Norway", Code = "NO"},
                new Country {Name = "Oman", Code = "OM"},
                new Country {Name = "Pakistan", Code = "PK"},
                new Country {Name = "Palau", Code = "PW"},
                new Country {Name = "Palestinian Authority", Code = "PS"},
                new Country {Name = "Panama", Code = "PA"},
                new Country {Name = "Papua New Guinea", Code = "PG"},
                new Country {Name = "Paraguay", Code = "PY"},
                new Country {Name = "Peru", Code = "PE"},
                new Country {Name = "Philippines", Code = "PH"},
                new Country {Name = "Pitcairn Islands", Code = "PN"},
                new Country {Name = "Poland", Code = "PL"},
                new Country {Name = "Portugal", Code = "PT"},
                new Country {Name = "Puerto Rico", Code = "PR"},
                new Country {Name = "Qatar", Code = "QA"},
                new Country {Name = "Republic of Côte d'Ivoire", Code = "CI"},
                new Country {Name = "Reunion", Code = "RE"},
                new Country {Name = "Romania", Code = "RO"},
                new Country {Name = "Russia", Code = "RU"},
                new Country {Name = "Rwanda", Code = "RW"},
                new Country {Name = "Saba", Code = "XS"},
                new Country {Name = "Saint Helena, Ascension and Tristan da Cunha", Code = "SH"},
                new Country {Name = "Samoa", Code = "WS"},
                new Country {Name = "San Marino", Code = "SM"},
                new Country {Name = "São Tomé and Príncipe", Code = "ST"},
                new Country {Name = "Saudi Arabia", Code = "SA"},
                new Country {Name = "Senegal", Code = "SN"},
                new Country {Name = "Serbia", Code = "RS"},
                new Country {Name = "Serbia, Montenegro", Code = "YU"},
                new Country {Name = "Seychelles", Code = "SC"},
                new Country {Name = "Sierra Leone", Code = "SL"},
                new Country {Name = "Singapore", Code = "SG"},
                new Country {Name = "Sint Eustatius", Code = "XE"},
                new Country {Name = "Sint Maarten", Code = "SX"},
                new Country {Name = "Slovakia", Code = "SK"},
                new Country {Name = "Slovenia", Code = "SI"},
                new Country {Name = "Solomon Islands", Code = "SB"},
                new Country {Name = "Somalia", Code = "SO"},
                new Country {Name = "South Africa", Code = "ZA"},
                new Country {Name = "South Georgia and the South Sandwich Islands", Code = "GS"},
                new Country {Name = "Spain", Code = "ES"},
                new Country {Name = "Sri Lanka", Code = "LK"},
                new Country {Name = "St. Barthélemy", Code = "BL"},
                new Country {Name = "St. Kitts and Nevis", Code = "KN"},
                new Country {Name = "St. Lucia", Code = "LC"},
                new Country {Name = "St. Martin", Code = "MF"},
                new Country {Name = "St. Pierre and Miquelon", Code = "PM"},
                new Country {Name = "St. Vincent and the Grenadines", Code = "VC"},
                new Country {Name = "Sudan", Code = "SD"},
                new Country {Name = "Suriname", Code = "SR"},
                new Country {Name = "Swaziland", Code = "SZ"},
                new Country {Name = "Sweden", Code = "SE"},
                new Country {Name = "Switzerland", Code = "CH"},
                new Country {Name = "Syria", Code = "SY"},
                new Country {Name = "Taiwan", Code = "TW"},
                new Country {Name = "Tajikistan", Code = "TJ"},
                new Country {Name = "Tanzania", Code = "TZ"},
                new Country {Name = "Thailand", Code = "TH"},
                new Country {Name = "Timor-Leste", Code = "TL"},
                new Country {Name = "Timor-Leste (East Timor)", Code = "TP"},
                new Country {Name = "Togo", Code = "TG"},
                new Country {Name = "Tokelau", Code = "TK"},
                new Country {Name = "Tonga", Code = "TO"},
                new Country {Name = "Trinidad and Tobago", Code = "TT"},
                new Country {Name = "Tristan da Cunha", Code = "TA"},
                new Country {Name = "Tunisia", Code = "TN"},
                new Country {Name = "Turkey", Code = "TR"},
                new Country {Name = "Turkmenistan", Code = "TM"},
                new Country {Name = "Turks and Caicos Islands", Code = "TC"},
                new Country {Name = "Tuvalu", Code = "TV"},
                new Country {Name = "Uganda", Code = "UG"},
                new Country {Name = "Ukraine", Code = "UA"},
                new Country {Name = "United Arab Emirates", Code = "AE"},
                new Country {Name = "United Kingdom", Code = "UK"},
                new Country {Name = "United States", Code = "US"},
                new Country {Name = "United States Minor Outlying Islands", Code = "UM"},
                new Country {Name = "Uruguay", Code = "UY"},
                new Country {Name = "Uzbekistan", Code = "UZ"},
                new Country {Name = "Vanuatu", Code = "VU"},
                new Country {Name = "Venezuela", Code = "VE"},
                new Country {Name = "Vietnam", Code = "VN"},
                new Country {Name = "Virgin Islands, British", Code = "VG"},
                new Country {Name = "Virgin Islands, U.S.", Code = "VI"},
                new Country {Name = "Wallis and Futuna", Code = "WF"},
                new Country {Name = "Yemen", Code = "YE"},
                new Country {Name = "Zambia", Code = "ZM"},
                new Country {Name = "Zimbabwe", Code = "ZW"}
            };

            context.Countries.AddOrUpdate(e => e.Id, countries);

            AddCauses(context);
            AddRiskFactors(context);
            AddFitnessActivities(context);
            AddHealthProfile(context);
            AddUserFitnessActivities(context);
        }


        public static void AddUserFitnessActivities(ApplicationDbContext context)
        {
            var user = context.Users.FirstOrDefault(x => x.Email == "gamunuud@gmail.com");

            var activity = context.FitnessActivities.First(x => x.Name == "Cricket");

            Random random = new Random();

            context.UserFitnessActivities.AddOrUpdate(new UserFitnessActivity
            {
                ActivityId = activity.Id,
                Distance = random.NextDouble() * random.Next(0, 50000),
                StartTime = DateTime.UtcNow,
                Energy = random.Next(1, 10000),
                EndTime = DateTime.UtcNow.AddMinutes(random.Next(1, 1440)),
                User = user,
                Steps = random.Next(0, 50000)
            });

            activity = context.FitnessActivities.First(x => x.Name == "Hiking");

            context.UserFitnessActivities.AddOrUpdate(new UserFitnessActivity
            {
                ActivityId = activity.Id,
                Distance = random.NextDouble() * random.Next(0, 50000),
                StartTime = DateTime.UtcNow.AddHours(-1),
                Energy = random.Next(1, 10000),
                EndTime = DateTime.UtcNow.AddMinutes(random.Next(1, 1440)),
                User = user,
                Steps = random.Next(0, 50000)
            });


            activity = context.FitnessActivities.First(x => x.Name == "Tennis");

            context.UserFitnessActivities.AddOrUpdate(new UserFitnessActivity
            {
                ActivityId = activity.Id,
                Distance = random.NextDouble() * random.Next(0, 50000),
                StartTime = DateTime.UtcNow.AddHours(-5),
                Energy = random.Next(1, 10000),
                EndTime = DateTime.UtcNow.AddMinutes(random.Next(1, 1440)),
                User = user,
                Steps = random.Next(0, 50000)
            });


            activity = context.FitnessActivities.First(x => x.Name == "Tennis");

            context.UserFitnessActivities.AddOrUpdate(new UserFitnessActivity
            {
                ActivityId = activity.Id,
                Distance = random.NextDouble() * random.Next(0, 50000),
                StartTime = DateTime.UtcNow.AddDays(-1),
                Energy = random.Next(1, 10000),
                EndTime = DateTime.UtcNow.AddMinutes(random.Next(1, 1440)),
                User = user,
                Steps = random.Next(0, 50000)
            });
        }

        private static void AddCauses(ApplicationDbContext context)
        {
            //Neoplasms
            context.Causes.AddOrUpdate(new Cause { Name = "Neoplasms", Status = "active" });
            context.SaveChanges();
            long id = context.Causes.First(x => x.Name == "Neoplasms").Id;

            context.Causes.AddOrUpdate(new Cause { Name = "Esophageal cancer", Status = "active", ParentCause = id },
                new Cause { Name = "Stomach cancer", Status = "active", ParentCause = id },
                new Cause { Name = "Larynx cancer", Status = "active", ParentCause = id },
                new Cause { Name = "Trachea, bronchus, and lung cancers", Status = "active", ParentCause = id },
                new Cause { Name = "Breast cancer", Status = "active", ParentCause = id },
                new Cause { Name = "Cervical cancer", Status = "active", ParentCause = id },
                new Cause { Name = "Uterine cancer", Status = "active", ParentCause = id },
                new Cause { Name = "Prostate cancer", Status = "active", ParentCause = id },
                new Cause { Name = "Colon and rectum cancers", Status = "active", ParentCause = id },
                new Cause { Name = "Mouth cancer", Status = "active", ParentCause = id },
                new Cause { Name = "Nasopharynx cancer", Status = "active", ParentCause = id },
                new Cause { Name = "Cancer of other part of pharynx and oropharynx", Status = "active", ParentCause = id },
                new Cause { Name = "Gallbladder and biliary tract cancer", Status = "active", ParentCause = id },
                new Cause { Name = "Pancreatic cancer", Status = "active", ParentCause = id },
                new Cause { Name = "Malignant melanoma of skin", Status = "active", ParentCause = id },
                new Cause { Name = "Non-melanoma skin cancer", Status = "active", ParentCause = id },
                new Cause { Name = "Ovarian cancer", Status = "active", ParentCause = id },
                new Cause { Name = "Testicular cancer", Status = "active", ParentCause = id },
                new Cause { Name = "Kidney and other urinary organ cancers", Status = "active", ParentCause = id },
                new Cause { Name = "Bladder cancer", Status = "active", ParentCause = id },
                new Cause { Name = "Brain and nervous system cancers", Status = "active", ParentCause = id },
                new Cause { Name = "Thyroid cancer", Status = "active", ParentCause = id },
                new Cause { Name = "Hodgkin's disease", Status = "active", ParentCause = id },
                new Cause { Name = "Non-Hodgkin lymphoma", Status = "active", ParentCause = id },
                new Cause { Name = "Multiple myeloma", Status = "active", ParentCause = id },
                new Cause { Name = "Leukemia", Status = "active", ParentCause = id },
                new Cause { Name = "Other neoplasms", Status = "active", ParentCause = id },
                new Cause { Name = "Liver cancer", Status = "active", ParentCause = id });
            context.SaveChanges();

            //Liver cancer
            long lid = context.Causes.First(x => x.Name == "Liver cancer").Id;
            context.Causes.AddOrUpdate(
                new Cause { Name = "Liver cancer secondary to hepatitis B", Status = "active", ParentCause = lid },
                new Cause { Name = "Liver cancer secondary to hepatitis C", Status = "active", ParentCause = lid },
                new Cause { Name = "Liver cancer secondary to alcohol use", Status = "active", ParentCause = lid },
                new Cause { Name = "Other liver cancer", Status = "active", ParentCause = lid });


            ////Cardiovascular and circulatory diseases
            context.Causes.AddOrUpdate(new Cause { Name = "Cardiovascular and circulatory diseases", Status = "active" });
            context.SaveChanges();
            id = context.Causes.First(x => x.Name == "Cardiovascular and circulatory diseases").Id;

            context.Causes.AddOrUpdate(
                new Cause { Name = "Rheumatic heart disease", Status = "active", ParentCause = id },
                new Cause { Name = "Ischemic heart disease", Status = "active", ParentCause = id },
                new Cause { Name = "Cerebrovascular disease", Status = "active", ParentCause = id },
                new Cause { Name = "Hypertensive heart disease", Status = "active", ParentCause = id },
                new Cause { Name = "Cardiomyopathy and myocarditis", Status = "active", ParentCause = id },
                new Cause { Name = "Atrial fibrillation and flutter", Status = "active", ParentCause = id },
                new Cause { Name = "Aortic aneurysm", Status = "active", ParentCause = id },
                new Cause { Name = "Peripheral vascular disease", Status = "active", ParentCause = id },
                new Cause { Name = "Endocarditis", Status = "active", ParentCause = id },
                new Cause { Name = "Other cardiovascular and circulatory diseases", Status = "active", ParentCause = id });

            //Cerebrovascular disease
            context.Causes.AddOrUpdate(new Cause { Name = "Cerebrovascular disease", Status = "active", ParentCause = id });
            context.SaveChanges();

            long cereb = context.Causes.First(x => x.Name == "Cerebrovascular disease").Id;
            context.Causes.AddOrUpdate(
                new Cause { Name = "Ischemic stroke", Status = "active", ParentCause = cereb },
                new Cause { Name = "Hemorrhagic and other non-ischemic stroke", Status = "active", ParentCause = cereb });

            ////Chronic respiratory diseases
            context.Causes.AddOrUpdate(new Cause { Name = "Chronic respiratory diseases", Status = "active" });
            context.SaveChanges();

            id = context.Causes.First(x => x.Name == "Chronic respiratory diseases").Id;
            context.Causes.AddOrUpdate(
                new Cause { Name = "Chronic obstructive pulmonary disease", Status = "active", ParentCause = id },
                new Cause { Name = "Pneumoconiosis", Status = "active", ParentCause = id },
                new Cause { Name = "Asthma", Status = "active", ParentCause = id },
                new Cause
                {
                    Name = "Interstitial lung disease and pulmonary sarcoidosis",
                    Status = "active",
                    ParentCause = id
                },

                new Cause
                {
                    Name = "Other chronic respiratory diseases",
                    Status = "active",
                    ParentCause = id
                });

            ////Cirrhosis of the liver
            context.Causes.AddOrUpdate(new Cause { Name = "Cirrhosis of the liver", Status = "active" });
            context.SaveChanges();

            id = context.Causes.First(x => x.Name == "Cirrhosis of the liver").Id;
            context.Causes.AddOrUpdate(
                new Cause
                {
                    Name = "Cirrhosis of the liver secondary to hepatitis B",
                    Status = "active",
                    ParentCause = id
                },
                new Cause
                {
                    Name = "Cirrhosis of the liver secondary to hepatitis C",
                    Status = "active",
                    ParentCause = id
                },
                new Cause
                {
                    Name = "Cirrhosis of the liver secondary to alcohol use",
                    Status = "active",
                    ParentCause = id
                },
                new Cause { Name = "Other cirrhosis of the liver", Status = "active", ParentCause = id });

            //Digestive diseases (except cirrhosis)
            context.Causes.AddOrUpdate(new Cause { Name = "Digestive diseases (except cirrhosis)", Status = "active" });
            context.SaveChanges();

            id = context.Causes.First(x => x.Name == "Digestive diseases (except cirrhosis)").Id;

            context.Causes.AddOrUpdate(new Cause { Name = "Peptic ulcer disease", Status = "active", ParentCause = id },
                new Cause { Name = "Gastritis and duodenitis", Status = "active", ParentCause = id },
                new Cause { Name = "Appendicitis", Status = "active", ParentCause = id },
                new Cause
                {
                    Name = "Paralytic ileus and intestinal obstruction without hernia",
                    Status = "active",
                    ParentCause = id
                },
                new Cause { Name = "Inguinal or femoral hernia", Status = "active", ParentCause = id },
                new Cause { Name = "Non-infective inflammatory bowel disease", Status = "active", ParentCause = id },
                new Cause { Name = "Vascular disorders of intestine", Status = "active", ParentCause = id },
                new Cause { Name = "Gall bladder and bile duct disease", Status = "active", ParentCause = id },
                new Cause { Name = "Pancreatitis", Status = "active", ParentCause = id },
                new Cause { Name = "Other digestive diseases", Status = "active", ParentCause = id });


            //Neurological disorders
            context.Causes.AddOrUpdate(new Cause { Name = "Neurological disorders", Status = "active" });
            context.SaveChanges();
            id = context.Causes.First(x => x.Name == "Neurological disorders").Id;

            context.Causes.AddOrUpdate(
                new Cause { Name = "Alzheimer's disease and other dementias", Status = "active", ParentCause = id },
                new Cause { Name = "Parkinson's disease", Status = "active", ParentCause = id },
                new Cause { Name = "Epilepsy", Status = "active", ParentCause = id },
                new Cause { Name = "Multiple sclerosis", Status = "active", ParentCause = id },
                new Cause { Name = "Migraine", Status = "active", ParentCause = id },
                new Cause { Name = "Tension-type headache", Status = "active", ParentCause = id },
                new Cause { Name = "Other neurological disorders", Status = "active", ParentCause = id });

            //Mental and behavioral disorders
            context.Causes.AddOrUpdate(new Cause { Name = "Mental and behavioral disorders", Status = "active" });
            context.SaveChanges();
            id = context.Causes.First(x => x.Name == "Mental and behavioral disorders").Id;

            context.Causes.AddOrUpdate(
                new Cause { Name = "Schizophrenia", Status = "active", ParentCause = id },
                new Cause { Name = "Alcohol use disorders", Status = "active", ParentCause = id },
                new Cause { Name = "Drug use disorders", Status = "active", ParentCause = id },
                new Cause { Name = "Unipolar depressive disorders", Status = "active", ParentCause = id },
                new Cause { Name = "Bipolar affective disorder", Status = "active", ParentCause = id },
                new Cause { Name = "Anxiety disorders", Status = "active", ParentCause = id },
                new Cause { Name = "Eating disorders", Status = "active", ParentCause = id },
                new Cause { Name = "Pervasive development disorders", Status = "active", ParentCause = id },
                new Cause { Name = "Childhood behavioral disorders", Status = "active", ParentCause = id },
                new Cause { Name = "Idiopathic intellectual disability',", Status = "active", ParentCause = id },
                new Cause { Name = "Other mental and behavioral disorders", Status = "active", ParentCause = id });
            context.SaveChanges();

            //Drug use disorders
            long drug = context.Causes.First(x => x.Name == "Drug use disorders").Id;
            context.Causes.AddOrUpdate(
                new Cause { Name = "Opioid use disorders", Status = "active", ParentCause = drug },
                new Cause { Name = "Cocaine use disorders", Status = "active", ParentCause = drug },
                new Cause { Name = "Amphetamine use disorders", Status = "active", ParentCause = drug },
                new Cause { Name = "Cannabis use disorders", Status = "active", ParentCause = drug },
                new Cause { Name = "Other drug use disorders", Status = "active", ParentCause = drug });


            //Unipolar depressive disorders
            context.SaveChanges();
            long uni = context.Causes.First(x => x.Name == "Unipolar depressive disorders").Id;
            context.Causes.AddOrUpdate(
                new Cause { Name = "Major depressive disorder", Status = "active", ParentCause = uni },
                new Cause { Name = "Dysthymia", Status = "active", ParentCause = uni });

            //Pervasive development disorders
            context.SaveChanges();
            long perv = context.Causes.First(x => x.Name == "Pervasive development disorders").Id;
            context.Causes.AddOrUpdate(
                new Cause { Name = "Autism", Status = "active", ParentCause = perv },
                new Cause { Name = "Asperger's syndrome", Status = "active", ParentCause = perv });

            //Pervasive development disorders
            context.SaveChanges();
            long child = context.Causes.First(x => x.Name == "Childhood behavioral disorders").Id;
            context.Causes.AddOrUpdate(
                new Cause { Name = "Attention-deficit hyperactivity disorder", Status = "active", ParentCause = child },
                new Cause { Name = "Conduct disorder", Status = "active", ParentCause = child });



            //Diabetes, urogenital, blood, and endocrine diseases
            context.Causes.AddOrUpdate(new Cause
            {
                Name = "Diabetes, urogenital, blood, and endocrine diseases",
                Status = "active"
            });
            context.SaveChanges();
            id = context.Causes.First(x => x.Name == "Diabetes, urogenital, blood, and endocrine diseases").Id;

            context.Causes.AddOrUpdate(
                new Cause { Name = "Diabetes mellitus", Status = "active", ParentCause = id },
                new Cause { Name = "Acute glomerulonephritis", Status = "active", ParentCause = id },
                new Cause { Name = "Chronic kidney diseases", Status = "active", ParentCause = id },

                new Cause { Name = "Urinary diseases and male infertility", Status = "active", ParentCause = id },
                new Cause { Name = "Gynecological diseases", Status = "active", ParentCause = id },
                new Cause { Name = "Hemoglobinopathies and hemolytic anemias", Status = "active", ParentCause = id },
                new Cause
                {
                    Name = "Other endocrine, nutritional, blood, and immune disorders",
                    Status = "active",
                    ParentCause = id
                });

            context.SaveChanges();


            //Chronic kidney diseases
            long chro = context.Causes.First(x => x.Name == "Chronic kidney diseases").Id;
            context.Causes.AddOrUpdate(
                new Cause
                {
                    Name = "Chronic kidney disease due to diabetes mellitus",
                    Status = "active",
                    ParentCause = chro
                },
                new Cause { Name = "Chronic kidney disease due to hypertension", Status = "active", ParentCause = chro },
                new Cause { Name = "Chronic kidney disease unspecified", Status = "active", ParentCause = chro });


            //Urinary diseases and male infertility
            long uri = context.Causes.First(x => x.Name == "Urinary diseases and male infertility").Id;
            context.Causes.AddOrUpdate(
                new Cause
                {
                    Name = "Tubulointerstitial nephritis, pyelonephritis, and urinary tract infections",
                    Status = "active",
                    ParentCause = uri
                },
                new Cause { Name = "Urolithiasis", Status = "active", ParentCause = uri },
                new Cause { Name = "Benign prostatic hyperplasia", Status = "active", ParentCause = uri },
                new Cause { Name = "Male infertility", Status = "active", ParentCause = uri },
                new Cause { Name = "Other urinary diseases", Status = "active", ParentCause = uri });

            //Gynecological diseases
            long gyn = context.Causes.First(x => x.Name == "Gynecological diseases").Id;
            context.Causes.AddOrUpdate(new Cause { Name = "Uterine fibroids", Status = "active", ParentCause = uri },
                new Cause { Name = "Polycystic ovarian syndrome", Status = "active", ParentCause = gyn },
                new Cause { Name = "Female infertility", Status = "active", ParentCause = gyn },
                new Cause { Name = "Endometriosis", Status = "active", ParentCause = gyn },
                new Cause { Name = "Genital prolapse", Status = "active", ParentCause = gyn },
                new Cause { Name = "Premenstrual syndrome", Status = "active", ParentCause = gyn },
                new Cause { Name = "Other gynecological diseases", Status = "active", ParentCause = gyn });


            //Hemoglobinopathies and hemolytic anemias
            long hemo = context.Causes.First(x => x.Name == "Hemoglobinopathies and hemolytic anemias").Id;
            context.Causes.AddOrUpdate(new Cause { Name = "Thalassemias", Status = "active", ParentCause = uri },
                new Cause { Name = "Sickle cell disorders", Status = "active", ParentCause = hemo },
                new Cause { Name = "G6PD deficiency", Status = "active", ParentCause = hemo },
                new Cause
                {
                    Name = "Other hemoglobinopathies and hemolytic anemias",
                    Status = "active",
                    ParentCause = hemo
                });




            //Musculoskeletal disorders
            context.Causes.AddOrUpdate(new Cause { Name = "Musculoskeletal disorders", Status = "active" });
            context.SaveChanges();
            id = context.Causes.First(x => x.Name == "Musculoskeletal disorders").Id;

            context.Causes.AddOrUpdate(
                new Cause { Name = "Rheumatoid arthritis", Status = "active", ParentCause = id },
                new Cause { Name = "Osteoarthritis", Status = "active", ParentCause = id },
                new Cause { Name = "Low back and neck pain", Status = "active", ParentCause = id },
                new Cause { Name = "Gout", Status = "active", ParentCause = id },
                new Cause { Name = "Other musculoskeletal disorders", Status = "active", ParentCause = id });
            context.SaveChanges();

            //Low back and neck pain
            long low = context.Causes.First(x => x.Name == "Low back and neck pain").Id;
            context.Causes.AddOrUpdate(new Cause { Name = "Low back pain", Status = "active", ParentCause = uri },
                new Cause { Name = "Neck pain", Status = "active", ParentCause = low });


            //Other non-communicable diseases
            context.Causes.AddOrUpdate(new Cause { Name = "Other non-communicable diseases", Status = "active" });
            context.SaveChanges();
            id = context.Causes.First(x => x.Name == "Other non-communicable diseases").Id;

            context.Causes.AddOrUpdate(
                new Cause { Name = "Congenital anomalies", Status = "active", ParentCause = id },
                new Cause { Name = "Skin and subcutaneous diseases", Status = "active", ParentCause = id },
                new Cause { Name = "Sense organ diseases", Status = "active", ParentCause = id },
                new Cause { Name = "Oral disorders", Status = "active", ParentCause = id },
                new Cause { Name = "Sudden infant death syndrome", Status = "active", ParentCause = id });

            context.SaveChanges();
            //Congenital anomalies
            long con = context.Causes.First(x => x.Name == "Congenital anomalies").Id;
            context.Causes.AddOrUpdate(new Cause { Name = "Neural tube defects", Status = "active", ParentCause = uri },
                new Cause { Name = "Congenital heart anomalies", Status = "active", ParentCause = con },
                new Cause { Name = "Cleft lip and cleft palate", Status = "active", ParentCause = con },
                new Cause { Name = "Down's syndrome", Status = "active", ParentCause = con },
                new Cause { Name = "Other chromosomal abnormalities", Status = "active", ParentCause = con },
                new Cause { Name = "Other congenital anomalies", Status = "active", ParentCause = con });


            context.SaveChanges();
            //Skin and subcutaneous diseases
            long skin = context.Causes.First(x => x.Name == "Skin and subcutaneous diseases").Id;
            context.Causes.AddOrUpdate(new Cause { Name = "Eczema", Status = "active", ParentCause = skin },
                new Cause { Name = "Psoriasis", Status = "active", ParentCause = skin },
                new Cause { Name = "Cellulitis", Status = "active", ParentCause = skin },
                new Cause
                {
                    Name = "Abscess, impetigo, and other bacterial skin diseases",
                    Status = "active",
                    ParentCause = skin
                },
                new Cause { Name = "Scabies", Status = "active", ParentCause = skin },
                new Cause { Name = "Fungal skin diseases", Status = "active", ParentCause = skin },
                new Cause { Name = "Viral skin diseases", Status = "active", ParentCause = skin },
                new Cause { Name = "Acne vulgaris", Status = "active", ParentCause = skin },
                new Cause { Name = "Alopecia areata", Status = "active", ParentCause = skin });

            context.SaveChanges();
            //Alopecia areata
            long alo = context.Causes.First(x => x.Name == "Alopecia areata").Id;
            context.Causes.AddOrUpdate(new Cause { Name = "Pruritus", Status = "active", ParentCause = alo },
                new Cause { Name = "Urticaria", Status = "active", ParentCause = alo },
                new Cause { Name = "Decubitus ulcer", Status = "active", ParentCause = alo },
                new Cause
                {
                    Name = "Other skin and subcutaneous diseases",
                    Status = "active",
                    ParentCause = alo
                });


            context.SaveChanges();
            //Sense organ diseases
            long sen = context.Causes.First(x => x.Name == "Sense organ diseases").Id;
            context.Causes.AddOrUpdate(new Cause { Name = "Glaucoma", Status = "active", ParentCause = sen },
                new Cause { Name = "Cataracts", Status = "active", ParentCause = sen },
                new Cause { Name = "Macular degeneration", Status = "active", ParentCause = sen },
                new Cause
                {
                    Name = "Refraction and accommodation disorders",
                    Status = "active",
                    ParentCause = sen
                },
                new Cause { Name = "Other hearing loss", Status = "active", ParentCause = sen },
                new Cause { Name = "Other vision loss", Status = "active", ParentCause = sen },
                new Cause { Name = "Other sense organ diseases", Status = "active", ParentCause = sen });

            context.SaveChanges();
            //Oral disorders
            long oral = context.Causes.First(x => x.Name == "Oral disorders").Id;
            context.Causes.AddOrUpdate(new Cause { Name = "Dental caries", Status = "active", ParentCause = oral },
                new Cause { Name = "Periodontal disease", Status = "active", ParentCause = oral },
                new Cause { Name = "Edentulism", Status = "active", ParentCause = oral });
        }

        private static void AddRiskFactors(ApplicationDbContext context)
        {
            //Unimproved water and sanitation
            context.RiskFactors.AddOrUpdate(new RiskFactor { Name = "Unimproved water and sanitation", Status = "active" });
            context.SaveChanges();
            long id = context.RiskFactors.First(x => x.Name == "Unimproved water and sanitation").Id;

            context.RiskFactors.AddOrUpdate(new RiskFactor { Name = "Unimproved water source", Status = "active", ParentRiskFactor = id },
                new RiskFactor { Name = "Unimproved sanitation", Status = "active", ParentRiskFactor = id });



            //Air pollution
            context.RiskFactors.AddOrUpdate(new RiskFactor { Name = "Air pollution", Status = "inactive" });
            context.SaveChanges();
            id = context.RiskFactors.First(x => x.Name == "Air pollution").Id;
            context.RiskFactors.AddOrUpdate(
                new RiskFactor { Name = "Ambient particulate matter pollution", Status = "active", ParentRiskFactor = id },
                new RiskFactor
                {
                    Name = "Household air pollution from solid fuels",
                    Status = "active",
                    ParentRiskFactor = id
                },
                new RiskFactor { Name = "Ambient ozone pollution", Status = "active", ParentRiskFactor = id });


            //Other environmental risks
            context.RiskFactors.AddOrUpdate(new RiskFactor { Name = "Other environmental risks", Status = "active" });
            context.SaveChanges();
            id = context.RiskFactors.First(x => x.Name == "Other environmental risks").Id;
            context.RiskFactors.AddOrUpdate(new RiskFactor { Name = "Residential radon", Status = "active", ParentRiskFactor = id },
                new RiskFactor { Name = "Lead exposure", Status = "active", ParentRiskFactor = id });



            //Child and maternal undernutrition
            context.RiskFactors.AddOrUpdate(new RiskFactor { Name = "Other environmental risks", Status = "active" });
            context.SaveChanges();
            id = context.RiskFactors.First(x => x.Name == "Other environmental risks").Id;
            context.RiskFactors.AddOrUpdate(new RiskFactor { Name = "Residential radon", Status = "active", ParentRiskFactor = id },
                new RiskFactor { Name = "Lead exposure", Status = "active", ParentRiskFactor = id });

            //Child and maternal undernutrition
            context.RiskFactors.AddOrUpdate(new RiskFactor
            {
                Name = "Child and maternal undernutrition",
                Status = "active"
            });
            context.SaveChanges();
            id = context.RiskFactors.First(x => x.Name == "Child and maternal undernutrition").Id;
            context.RiskFactors.AddOrUpdate(new RiskFactor { Name = "Suboptimal breastfeeding", Status = "active", ParentRiskFactor = id },
                new RiskFactor { Name = "Childhood underweight", Status = "active", ParentRiskFactor = id },
                new RiskFactor { Name = "Iron deficiency", Status = "active", ParentRiskFactor = id },
                new RiskFactor { Name = "Vitamin A deficiency", Status = "active", ParentRiskFactor = id },
                new RiskFactor { Name = "Zinc deficiency", Status = "active", ParentRiskFactor = id });



            //Tobacco smoking
            context.RiskFactors.AddOrUpdate(new RiskFactor { Name = "Tobacco smoking", Status = "active" });
            context.SaveChanges();
            id = context.RiskFactors.First(x => x.Name == "Tobacco smoking").Id;
            context.RiskFactors.AddOrUpdate(
                new RiskFactor { Name = "Tobacco smoking, excluding second-hand smoke", Status = "active", ParentRiskFactor = id },
                new RiskFactor { Name = "Second-hand smoke", Status = "active", ParentRiskFactor = id });


            //Alcohol and drug use
            context.RiskFactors.AddOrUpdate(new RiskFactor { Name = "Alcohol and drug use", Status = "active" });
            context.SaveChanges();
            id = context.RiskFactors.First(x => x.Name == "Alcohol and drug use").Id;
            context.RiskFactors.AddOrUpdate(new RiskFactor { Name = "Alcohol use", Status = "active", ParentRiskFactor = id },
                new RiskFactor { Name = "Drug use", Status = "active", ParentRiskFactor = id });



            //Physiological risks
            context.RiskFactors.AddOrUpdate(new RiskFactor { Name = "Physiological risks", Status = "inactive" });
            context.SaveChanges();
            id = context.RiskFactors.First(x => x.Name == "Physiological risks").Id;
            context.RiskFactors.AddOrUpdate(new RiskFactor { Name = "High fasting plasma glucose", Status = "active", ParentRiskFactor = id },
                new RiskFactor { Name = "High total cholesterol", Status = "active", ParentRiskFactor = id },
                new RiskFactor { Name = "High blood pressure", Status = "active", ParentRiskFactor = id },
                new RiskFactor { Name = "High body-mass index", Status = "active", ParentRiskFactor = id },
                new RiskFactor { Name = "Low bone mineral density", Status = "active", ParentRiskFactor = id });

            //Dietary risks
            context.RiskFactors.AddOrUpdate(new RiskFactor { Name = "Dietary risks", Status = "active" });
            context.SaveChanges();
            id = context.RiskFactors.First(x => x.Name == "Dietary risks").Id;
            context.RiskFactors.AddOrUpdate(
                new RiskFactor { Name = "Diet low in fruits", Status = "active", ParentRiskFactor = id },
                new RiskFactor { Name = "Diet low in vegetables", Status = "active", ParentRiskFactor = id },
                new RiskFactor { Name = "Diet low in whole grains", Status = "active", ParentRiskFactor = id },
                new RiskFactor { Name = "Diet low in nuts and seeds", Status = "active", ParentRiskFactor = id },
                new RiskFactor { Name = "Diet low in milk", Status = "active", ParentRiskFactor = id },
                new RiskFactor { Name = "Diet low in red meat", Status = "active", ParentRiskFactor = id },
                new RiskFactor { Name = "Diet high in precessed meat", Status = "active", ParentRiskFactor = id },
                new RiskFactor
                {
                    Name = "Diet high in sugar-sweetened beverages",
                    Status = "active",
                    ParentRiskFactor = id
                },
                new RiskFactor { Name = "Diet low in fiber", Status = "active", ParentRiskFactor = id },
                new RiskFactor { Name = "Diet low in calcium", Status = "active", ParentRiskFactor = id },
                new RiskFactor
                {
                    Name = "Diet low in seafood omega-3 fatty acids",
                    Status = "active",
                    ParentRiskFactor = id
                },
                new RiskFactor
                {
                    Name = "Diet low in polyunsaturated fatty acids",
                    Status = "active",
                    ParentRiskFactor = id
                },
                new RiskFactor { Name = "Diet high in trans fattry acids", Status = "active", ParentRiskFactor = id },
                new RiskFactor { Name = "Diet high in sodium", Status = "active", ParentRiskFactor = id });

            //Physical inactivity and low physical activity
            context.RiskFactors.AddOrUpdate(new RiskFactor
            {
                Name = "Physical inactivity and low physical activity",
                Status = "active"
            });
            context.SaveChanges();
            id = context.RiskFactors.First(x => x.Name == "Physical inactivity and low physical activity").Id;
            context.RiskFactors.AddOrUpdate(new RiskFactor { Name = "Occypational carcinogens", Status = "active", ParentRiskFactor = id },
                new RiskFactor { Name = "Occupational asthmagens", Status = "active", ParentRiskFactor = id },
                new RiskFactor { Name = "Occupational particulate matter, gases, and fumes", Status = "active", ParentRiskFactor = id },
                new RiskFactor { Name = "Occupational noise", Status = "active", ParentRiskFactor = id },
                new RiskFactor { Name = "Occupational risk factors for injuries", Status = "active", ParentRiskFactor = id },
                new RiskFactor { Name = "Occupational low back pain", Status = "active", ParentRiskFactor = id });


            //Sexual abuse and violence
            context.RiskFactors.AddOrUpdate(new RiskFactor { Name = "Sexual abuse and violence", Status = "active" });
            context.SaveChanges();
            id = context.RiskFactors.First(x => x.Name == "Sexual abuse and violence").Id;
            context.RiskFactors.AddOrUpdate(new RiskFactor { Name = "Childhood sexual abuse", Status = "active", ParentRiskFactor = id },
                new RiskFactor { Name = "Intimate partner violence", Status = "active", ParentRiskFactor = id });
        }

        private static void AddFitnessActivities(ApplicationDbContext context)
        {
            var activites = new[]
            {
                new FitnessActivity {Name = "Aerobics"}, 
                new FitnessActivity {Name = "Autralian football"}, 
                new FitnessActivity {Name = "Backcountry skiing"}, 
                new FitnessActivity {Name = "Badminton"}, 
                new FitnessActivity {Name = "Baseball"}, 
                new FitnessActivity {Name = "Beach volleyball"}, 
                new FitnessActivity {Name = "Biathlon"}, 
                new FitnessActivity {Name = "Biking"}, 
                new FitnessActivity {Name = "Boxing"}, 
                new FitnessActivity {Name = "Calisthenics"}, 
                new FitnessActivity {Name = "Circuit traning"}, 
                new FitnessActivity {Name = "Cricket"}, 
                new FitnessActivity {Name = "Cross skating"}, 
                new FitnessActivity {Name = "Cross-country skiing"}, 
                new FitnessActivity {Name = "Curling"}, 
                new FitnessActivity {Name = "Dancing"}, 
                new FitnessActivity {Name = "Diving"}, 
                new FitnessActivity {Name = "Downhill skiing"}, 
                new FitnessActivity {Name = "Elliptical"}, 
                new FitnessActivity {Name = "Ergometer"}, 
                new FitnessActivity {Name = "Fencing"}, 
                new FitnessActivity {Name = "Fitness walking"}, 
                new FitnessActivity {Name = "Football"}, 
                new FitnessActivity {Name = "Frisbee"}, 
                new FitnessActivity {Name = "Gardening"}, 
                new FitnessActivity {Name = "Golf"}, 
                new FitnessActivity {Name = "Gymnastics"}, 
                new FitnessActivity {Name = "Handball"}, 
                new FitnessActivity {Name = "Handcycling"}, 
                new FitnessActivity {Name = "Hiking"}, 
                new FitnessActivity {Name = "Hockey"}, 
                new FitnessActivity {Name = "Horseback riding"}, 
                new FitnessActivity {Name = "Ice skating"}, 
                new FitnessActivity {Name = "Indoor skating"}, 
                new FitnessActivity {Name = "Indoor volleyball"}, 
                new FitnessActivity {Name = "Inline skating"}, 
                new FitnessActivity {Name = "Jogging"}, 
                new FitnessActivity {Name = "Jumping rope"}, 
                new FitnessActivity {Name = "Kayaking"}, 
                new FitnessActivity {Name = "Kettlebell"}, 
                new FitnessActivity {Name = "Kickboxing"}, 
                new FitnessActivity {Name = "Kite skiing"}, 
                new FitnessActivity {Name = "Kitesurfing"}, 
                new FitnessActivity {Name = "Martial arts"}, 
                new FitnessActivity {Name = "Mixed martial arts"}, 
                new FitnessActivity {Name = "Mountain biking"}, 
                new FitnessActivity {Name = "Nordic walking"}, 
                new FitnessActivity {Name = "Open water swimming"}, 
                new FitnessActivity {Name = "Other"}, 
                new FitnessActivity {Name = "P90x"}, 
                new FitnessActivity {Name = "Paddle boarding"}, 
                new FitnessActivity {Name = "Paragliding"}, 
                new FitnessActivity {Name = "Pilates"}, 
                new FitnessActivity {Name = "Polo"}, 
                new FitnessActivity {Name = "Pool Swimming"}, 
                new FitnessActivity {Name = "Racquetball"}, 
                new FitnessActivity {Name = "Road biking"}, 
                new FitnessActivity {Name = "Rock climbing"}, 
                new FitnessActivity {Name = "Roller skiing"}, 
                new FitnessActivity {Name = "Rowing"}, 
                new FitnessActivity {Name = "Rowing machine"}, 
                new FitnessActivity {Name = "Rugby"}, 
                new FitnessActivity {Name = "Running"}, 
                new FitnessActivity {Name = "Sand running"}, 
                new FitnessActivity {Name = "Scuba diving"}, 
                new FitnessActivity {Name = "Skateboarding"}, 
                new FitnessActivity {Name = "Skating"}, 
                new FitnessActivity {Name = "Skiing"}, 
                new FitnessActivity {Name = "Sledding"}, 
                new FitnessActivity {Name = "Snowboarding"}, 
                new FitnessActivity {Name = "Snowshoeing"}, 
                new FitnessActivity {Name = "Soccer"}, 
                new FitnessActivity {Name = "Spinning"}, 
                new FitnessActivity {Name = "Squash"}, 
                new FitnessActivity {Name = "Stair climbing"}, 
                new FitnessActivity {Name = "Stair climbing machine"}, 
                new FitnessActivity {Name = "Stationary biking"}, 
                new FitnessActivity {Name = "Strength training"}, 
                new FitnessActivity {Name = "Surfing"}, 
                new FitnessActivity {Name = "Swimming"}, 
                new FitnessActivity {Name = "Table tennis"}, 
                new FitnessActivity {Name = "Tennis"}, 
                new FitnessActivity {Name = "Treadmill running"}, 
                new FitnessActivity {Name = "Treadmill walking"}, 
                new FitnessActivity {Name = "Utitliy biking"}, 
                new FitnessActivity {Name = "Volleyball"}, 
                new FitnessActivity {Name = "Wakeboarding"}, 
                new FitnessActivity {Name = "Walking"}, 
                new FitnessActivity {Name = "Water polo"}, 
                new FitnessActivity {Name = "Weight lifting"}, 
                new FitnessActivity {Name = "Wheelchair"}, 
                new FitnessActivity {Name = "Windsurfing"}, 
                new FitnessActivity {Name = "Yoga"}, 
                new FitnessActivity {Name = "Zumba"}
            };
            context.FitnessActivities.AddOrUpdate(activites);
        }

        private static void AddHealthProfile(ApplicationDbContext context)
        {
            var user = context.Users.FirstOrDefault();

            context.HealthProfiles.AddOrUpdate(new HealthProfile()
            {
                FitnessGloalCaloriesBurned = 1,
                FitnessGloalDistance = 1,
                FitnessGloalDuration = 1,
                FitnessGloalSteps = 1,
                User = user
            });
        }
    }
}
