/* 
GMTK Game Jam 2024
Theme: Built to Scale
Gameplay: Build robots based on customer commissions. Scale up robots and expand business.
Writing needs: Dialogue
*/

VAR build_success = true

== mayor ==
{ 
  - not guyd_bot.tutorial: -> game_intro
}
= game_intro
Congratulations on your new shop! As mayor, I'm always eager to support small businesses in this town! # speaker mayor
Speaking of support, I need your help. # speaker mayor
City Hall is literally a stinking mess today - burst sewage pipe!  Could you send a bot to take care of the situation? # speaker mayor
-> guyd_bot.tutorial

== guyd_bot ==
= tutorial
New day, new beginnings, I say! Let me show you around! # speaker guyd 
Fear not, I was Best Teaching Assistant five years in a row before this. You're in good hands! # speaker guyd
// What's in the tutorial?
-> DONE

== builder_bot ==
{
  - build_success: -> success
  - else: -> try_again
}
= success
Great job, bot is coming online now!
-> DONE 

= try_again
Uh-oh, did you check the schematics?
-> DONE

