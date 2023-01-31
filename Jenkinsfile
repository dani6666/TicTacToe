pipeline {
    agent any

    environment {
		DOCKERHUB_CREDENTIALS=credentials('dockerhubaccount')
	}

    stages {
        stage('Build') {
            steps {
                sh 'chmod +x build-images.sh'
                sh 'sudo ./build-images.sh'
            }
        }

        stage('Push image') {
            steps {
                sh 'echo $DOCKERHUB_CREDENTIALS_PSW | docker login -u $DOCKERHUB_CREDENTIALS_USR --password-stdin'
            }
        } 

        stage('Push') {

			steps {
				sh 'sudo docker push dani666/tictactoe-back:latest'
                sh 'sudo docker push dani666/tictactoe-front:latest'
                sh 'sudo docker push dani666/tictactoe-router:latest'
			}
		}

    }
}