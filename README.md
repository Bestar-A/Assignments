# HealthCare Management & Student Register

This is C# project for Patients Management and Student Register.  

## E-HealthCare
This is `Patients Management App` for storing patients data by using `SQL Server`.  
Most of Windows Systems have builtin `LocalDB`.  
I think you can check the code in `eHealthCare` of this repo.
### NOTE 1
This is main project.  
You can View database file with `SSMS`.
### NOTE 2
I have attached database file named `Patients.mdf` and `Queries.docx` to view SQL Query.  
SQL Queries go here.  
-	Create Patient table Query
```
CREATE TABLE Patients (PatientNo NVARCHAR(50) PRIMARY KEY, Fullname NVARCHAR(50) NOT NULL, PatientType NVARCHAR(50) NOT NULL, Gender NVARCHAR(50) NOT NULL, Illness NVARCHAR(50) NOT NULL, PhoneNumber NVARCHAR(50) NOT NULL, Province NVARCHAR(50) NOT NULL, DOB NVARCHAR(50) NOT NULL)
```
-	Insert Patient Data into table Query
```
INSERT INTO (PatientNo, Fullname, PatientType, Gender, Illness, PhoneNumber, Province, DOB) VALUES (@patientNoVal, @fullnameVal, @patientTypeVal, @genderVal, @illnessVal, @phoneNumberVal, @provinceVal, @DOBVal)
```
-	Get All Patients from table Query
```
SELECT * FROM Patients
```
-	Search Patient from table Query
```
SELECT * FROM Patients WHERE PatientNo=@PatientNo
```
-	Delete Patient Data from table Query
```
DELETE FROM Patients WHERE PatientNo=@PatientNo
```

## Eduvos Register
This is `Eduvos Student Register App` for storing students data.  
This app used normal txt file instead of database file for storing data.

## Contact Me
My Skype Name: `live:.cid.9d6e57af4c90cd35`  

I am happy to work for you. I will be here for you, all the time.  
*Deer Kareem* 😄
