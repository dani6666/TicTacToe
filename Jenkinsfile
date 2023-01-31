pipeline {
    agent any

    environment {
		DOCKERHUB_CREDENTIALS=credentials('dockerhubaccount')
	}

    stages {
        stage('Build') {
            steps {
                sh './build-images.sh'
            }
        }

        stage('Push image') {
            steps {
                sh 'echo $DOCKERHUB_CREDENTIALS_PSW | docker login -u $DOCKERHUB_CREDENTIALS_USR --password-stdin'
            }
        } 

        stage('Push') {

			steps {
				sh 'docker push dani666/tictactoe-back:latest'
                sh 'docker push dani666/tictactoe-front:latest'
                sh 'docker push dani666/tictactoe-router:latest'
			}
		}

    }
}