# TicTacToe - AWS - Daniel Włudarczyk

## Aplikacja

Aplikacja składa się z API napisanego w .NET 6 w C#. Api korzysta z bazy in-memory. Frontend jest napisany w angularze, który w dockerze jest hostowany przez serwer nginx.

Na potrzeby wdrożenia do aplikacji 

### Publikacja kontenerów

Wszystkie 3 kontenery zostały opublikowane na DockerHub komendami:
```
docker build . -t <name>
docker run <name>
docker push <name>
```

## Wdrożenie na AWS

Amazon Elastic Beanstalk
Create Application  
* Docker  
* ECS running on Linux  
* Name: ...  
* Your code: -> Upload file
* Source code origin Dockerrun.aws.json  

Configure more options  
* Security  
    * Service role: LabRole
    * EC2 key pair: vockey
    * IAM instance profile: LabInstanceRole

* Database:
    * Snapshot: None
    * Engine: mysql
    * Storage: 5 GB
    * Username: tictactoeuser
    * Password: tictactoepassword
    * Availability: Low
    * Deletion policy: Delete

After creating
* Configuration -> Software -> Environment properties:
    * DB_HOST ...
    * DB_PORT 3306
    * DB_NAME ebdb
    * DB_USER ...
    * DB_PASSWORD ...
* Read values from Rds -> Databases Connetivity and Security / Configuration
* Apply
* Redepoly

To delete:
* Rds
* Beanstalk -> Terminate env
* Beanstalk -> Delete app

## Backup bazy (failed)

AWS Backup -> Dashboard -> Create on-demand backup:
* Resource type: RDS
* Choose db
* Backup window: Create backup now
* Retention period: Always
* Backup vault: Default
* IAM role: LabRole

Error:
IAM Role arn:aws:iam::295012466370:role/LabRole does not have sufficient permissions to execute the backup.

## Dodanie pozwoleń na tworzenie backupu (failed)

IAM > Roles -> LabRole -> Add permisions -> Attach inline policy:
* Service: Backup
* Actions: All
* Resource: All

Error:
User: ... is not authorized to perform: iam:PutRolePolicy on resource: role LabRole because no identity-based policy allows the iam:PutRolePolicy action

## Restore bazy z backupu (failed)

AWS Backup -> Backup vaults -> Select backup -> Restore
* DB engine: MySQL
* License Model: generala-public
* Multi AZ: No
* Storage type: Provisioned IOPS (SSD)
* Provisioned IOPS: 3000
* DB instance identifier: ...
* Resotore role: LabRole
Restore backup

Error:
(nie na backupu do wybrania, na Default akcja Restore jest niedostępna)


## Konfiguracja Cloudwatch

CloudWatch -> All metrics -> RDS -> All databases -> CPU utilization
Graphed metrics -> Add alarm
Threshold value: ...
Alarm state trigger: In alarm 
Create new topic
Email endpoints: @student.pwr.edu.pl
Create topic
Alarm name: ...

Check if subscription is confirmed:
Amazon SNS -> Subscriptions

Confirm subscription in email

## Konfiguracja CloudTrail

Create trail
Select trail -> Inisght events -> Edit
Choose Insights types: API call rate & API error rate
Save

Check events:
Selected trail -> Trail log location
CloudTrail -> Dashboard -> Event history 

## Dodanie AWS Inspector (failed)

Amazon Inspector -> Get Started -> Activate Inspector -> 
Error:
Cannot activate account due to insufficient permissions. Add the following permissions: inspector2:Enable

Dashboard -> Instances with most critical findings -> Select

## Dodanie AWS Inspector (classic)

https://console.aws.amazon.com/inspector/.

Run assesment once