/* 
GMTK Game Jam 2024
Theme: Built to Scale
Gameplay: Build robots based on customer commissions. Scale up robots and expand business.
Writing needs: Dialogue
*/

VAR build_success = true
VAR cook_success = true
VAR cowboy_success = true
VAR kitty_success = true
VAR fire_success = true
VAR battle_success = true

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
Fear not, I was Best Teaching Assistant five years in a row. You're in good hands! # speaker guyd
// What's in the tutorial?
-> DONE

= scan_jobs
Hmm, slow day. I wonder what's going on in town... # speaker guyd
-> DONE

== builder_bot ==
{
  - build_success: -> success
  - else: -> try_again
}

= success
Ready to build, boss!
-> DONE 

= try_again
Uh-oh, are these the right schematics?
-> DONE

== cook_bot ==
{
  - cook_success: -> can_cook
  - else: -> cannot_cook
}

= can_cook
I'll cook my way to your heart!
-> DONE

= cannot_cook
I don't know how to boil water... help!
-> DONE

== cowboy_bot ==
{
  - cowboy_success: -> rodeo
  - else: -> no_rodeo
}

= rodeo
-> DONE

= no_rodeo
-> DONE

== kitty_bot ==
{
  - kitty_success: -> is_cat
  - else: -> not_cat
}

= is_cat
-> DONE

= not_cat
-> DONE

== fire_bot ==
{
  - fire_success: -> hot
  - else: -> not_hot
}

= hot
-> DONE

= not_hot
-> DONE

== battle_bot ==
{
  - battle_success: -> fight
  - else: -> no_fight
}

= fight
-> DONE

= no_fight
-> DONE




