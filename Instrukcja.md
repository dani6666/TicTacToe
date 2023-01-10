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
    * Username: USERNAME
    * Password: PASSWORD
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
