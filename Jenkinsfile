pipeline {
    agent any

    environment {
		DOCKERHUB_CREDENTIALS=credentials('dockerhubaccount')
	}

    stages {
        stage('Build') {
            steps {
                sh 'sudo docker build ./TicTacToeBack/src -t dani6666/tictactoe-api'
                sh 'sudo docker build ./TicTacToeFront -t dani6666/tictactoe-ui'
                sh 'sudo docker build ./Router -t dani6666/tictactoe-router'
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