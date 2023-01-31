pipeline {
    agent {label 'EC2 Jenkins Agent'}

    environment {
		DOCKERHUB_CREDENTIALS=credentials('dockerhubaccount')
	}

    stages {
        stage('Build') {
            steps {
                sh 'sudo systemctl start docker'
                sh 'sudo docker build ./TicTacToeBack/src -t dani6666/tictactoe-back'
                sh 'sudo docker build ./TicTacToeFront -t dani6666/tictactoe-front'
                sh 'sudo docker build ./Router -t dani6666/tictactoe-router'
            }
        }

        stage('Push image') {
            steps {
                sh 'echo $DOCKERHUB_CREDENTIALS_PSW | sudo docker login -u $DOCKERHUB_CREDENTIALS_USR --password-stdin'
            }
        } 

        stage('Push') {

			steps {
				sh 'sudo docker push dani6666/tictactoe-back:latest'
                sh 'sudo docker push dani6666/tictactoe-front:latest'
                sh 'sudo docker push dani6666/tictactoe-router:latest'
			}
		}

        stage('Deploy') {
            steps {
                sh 'sudo aws configure set region "us-east-1"'
                sh 'sudo aws elasticbeanstalk update-environment --environment-name Tictactoe-env --version-label tictactoe-source'
            }
        }
    }
}
