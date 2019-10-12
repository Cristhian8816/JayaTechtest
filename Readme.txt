The Apllications works in the following form:

. The index view show you a list of chatrooms actives and you can choose in which one you want to get in, or you can create a new chatroom with a specific conversation´s topic.

. when you choose a chat room, the next view ask you for your Nickname, and when you put it the system validate if this nickname is used and active.

. if your nickname is available to use, the application set the comunication with the server and you can start to chat

.everything that everybody writes in all chatrooms is saved in a database and you can admin this messages int the AdminModel.

.you can get in the AdminModel clicking in a "login" label that is in the top of the view.

.this module show you a form login where you have to put the followings credentials
		
		username: jayaUser
		password: 1234
	is case sensitive so put it equal!!

. after you login in the system you are going to see two dropdownlists, the firstone show you all users register in the system and you can click the botton below to consult his all history messages written.
  the second dropdownlist show you all chat rooms, and in the button below you can consult the history of the messages of this chat room for each user register in this Chat.



Instruccion to run the app.

.firstly you have to restore de database, to do this you have to use the file jaya_Chat.bak or you can run the script jaya_Chat.sql

.after, you have to run the server, to do this you have to run de ChatRoomServer project

.the last step is run the jayatechTest project.


enjoy!!!
