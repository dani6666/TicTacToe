sudo docker build ./TicTacToeBack/src -t dani6666/tictactoe-api
sudo docker build ./TicTacToeFront -t dani6666/tictactoe-ui
sudo docker build ./reverse-proxy -t dani6666/tictactoe-proxy


# docker push dani6666/tictactoe-api 
# docker push dani6666/tictactoe-ui 
# docker push dani6666/tictactoe-proxy 