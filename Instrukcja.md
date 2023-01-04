# TicTacToe - Daniel Włudarczyk

## Aplikacja

Aplikacja składa się z API napisanego w .NET 6 w C#. Api korzysta z bazy in-memory. Frontend jest napisany w angularze, który w dockerze jest hostowany przez serwer nginx.

## Wdrożenie na AWS

Create repository
private

Set-ExecutionPolicy -ExecutionPolicy Undefined
Import-Module AWS.Tools.CommonAWS.Tools.Common nie działa:
    The specified module 'AWS.Tools.CommonAWS.Tools.Common' was not loaded because no valid module file
    was found in any module directory.

Amazon Elastic Beanstalk
Create Application
Docker 
ECS running...
name, 
your code - file
Configura more option
Security
Service role: LabRole
EC2 key pair: vockey
IAM instance profile: LabInstanceRole

Role: LabRole
key pair: vockey
profile: LabInstanceProfile


Creating Auto Scaling group failed Reason: API: autoscaling:CreateAutoScalingGroup User: arn:aws:sts::295012466370:assumed-role/voclabs/user2253007=W__udarczyk_Daniel_Adam is not authorized to perform: autoscaling:CreateAutoScalingGroup on resource: arn:aws:autoscaling:us-east-1:295012466370:autoScalingGroup:*:autoScalingGroupName/awseb-e-rzesmbyzmk-stack-AWSEBAutoScalingGroup-ZNL7EEJ55X2F because no identity-based policy allows the autoscaling:CreateAutoScalingGroup action


<!-- Nie może stworzyć środowiska:
You do not have enough permissions. Failed to create default instance profile: User: arn:aws:sts::295012466370:assumed-role/voclabs/user2253007=W__udarczyk_Daniel_Adam is not authorized to perform: iam:CreateRole on resource: arn:aws:iam::295012466370:role/aws-elasticbeanstalk-ec2-role because no identity-based policy allows the iam:CreateRole action (Service: AmazonIdentityManagement; Status Code: 403; Error Code: AccessDenied; Request ID: bfe14271-27a7-4f7c-aa23-673c971a8f8a; Proxy: null) -->


### Publikacja kontenerów

Publikujemy oba pliki Dockerfile jako kontenery na DockerHuba kolejno komendami
```
docker build . -t <name>
docker run <name>
docker push <name>
```

### Tworzenie Elastic Container Service

W konsoli AWS wyszukać serwis `Elastic Container Service` i przejść następującą ścieżką:  
Wejść w zakładke `Clusters` i kliknąć `Create cluster`,  
Wybrać opcje `EC2 Linux + Networking`, klikąć `Next page ` 
Wypełnić następujące pola:  
* Cluster name: `nazwa klastra`
* EC2 instance type: `m6id.large`
* Container instance IAM role: `LabRole` (lub inną adekwatna rolę)

### Tworzenie Task Definition

Po utworzeniu klastra wyświetlić go (`View cluster`) i przejść do `Task Definitions`  
Stworzyć nową task definition (`Create new task definition`) wybierając opcję `Fargate` i przejśc dalej  
Następnie wepłnić następujące pola:
* Task definition name: `nazwa task definition`
* Task role: `LabRole`
* Operating system family: `Linux`
* Task memory (GB): `0.5 GB`
* Task CPU (vCPU): `0.25 vCPU`

Dodać oba kontenery (`Add container`) wypełniając:
* Container name: `nazwa kontenera`
* Image: `dani6666/tictactoe-front:latest` dla frontendu oraz `dani6666/tictactoe-back:latest` dla backendu
* Memory Limits: `Soft limit`, `256`
* Port mappings: `4200` dla frontendu oraz `8000` dla backendu  

Potwierdzić stworzenie task definition (`Create -> View task definition`)

### Tworzenie Load Balancera

Przechodzimy do `Load balancers (EC2 service)` w `Services`  
Wybieramy `Create load balancer -> Application load balancer`
Wypełniamy następujące pola:
* Load balancer name: `nazwa load balancera`
* VPC: `utworzona przez nas VPC`
* Mappings: zaznaczamy wszystkie
* Security groups: utworzone przez nas security grupa

W `Listeners and routing` ustawiamy protokuł `HTTP` i port `4200` i tworzymy target grupe:
* Wybieramy typ `IP adresses`
* Target group name: `nazwa target grupy`
* Posrt: 4200
Klikamy `next` i wypełniamy:
* IPv4 address: `10.0.0.1`

Tworzymy grupe, wybieramy ją przy tworzeniu load balancera i tworzymy load balancer.

### Tworzenie Serwisu

Powrócić do widoku utworzonego klastra (`Clusters -> wybrać utworzony klaster`)  
Wchodzimy w zakładke `Services` i klikamy `Create service`.  
Wypełniamy pola:
* Launch type: `FARGATE`
* Task Definition: utworzony task definition
* Service name: `nazwa serwisu`
* Number of tasks: `2`
Przechodzimy dalej i wypełniamy następnie pola:
* Cluster VPC: vpc na którym został utworzony load balancer
* Network subnet: wszystkimi dostępnymi podsieciami
* Security groups: `Edit` i wybieramy utworzoną security grupe
* Container to load balance: utworzony kontener i klikamy `Add to load balancer`
* Target group name: utworzona target grupa

Przechodzimy dwa kolejne kroki nic nie zmienając i klikamy `Create service`, a następnie `View service`

### Test

Czekamy aż status tasków w serwisie zmieni się na `RUNNING` (`Cluster->Service->Tasks`) a status Load balancera na `Active` (`Load balancers->Description->State`)

Po skopiowaniu urla (`Load balancers->Description->DNS name`) load balancer odrzuca połączenie, ale na monitoring load balancera widać requesty (`Load balancers->Monitoring->Requests`)