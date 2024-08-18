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
City Hall is a stinking mess today - turns out a nest of rats have been living in our vents!  Could you send someone to take care of the situation? # speaker mayor
-> guyd_bot.tutorial

= host_event
Hello, your shop seems to be doing well! My office pushed for those tax incentives in the last election - we're big on helping businesses! # speaker mayor
Anyway, we're hosting a delegation from our sister city this week and our regular caterers are overbooked. Do you have a team that can prepare Greek food?
-> DONE

== fire_chief ==
= forest_fire
There's a forest fire and it's heading towards town. My people need all the help they can get. Give me your best firefighters! # speaker fire_chief
-> DONE

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
Help! How do you boil water?
-> DONE

== cowboy_bot ==
{
  - cowboy_success: -> rodeo
  - else: -> no_rodeo
}

= rodeo
This ain't my first rodeo. Giddy up!
-> DONE

= no_rodeo
Why am I riding a horse? 
-> DONE

== kitty_bot ==
{
  - kitty_success: -> is_cat
  - else: -> not_cat
}

= is_cat
Meow!
-> DONE

= not_cat
I don't hunt. Who do you think I am, a cat?
-> DONE

== fire_bot ==
{
  - fire_success: -> hot
  - else: -> not_hot
}

= hot
There's no smoke without... FIRE! I'll take care of this!
-> DONE

= not_hot
I'm smoking... and I don't mean hot.
-> DONE

== battle_bot ==
{
  - battle_success: -> fight
  - else: -> no_fight
}

= fight
Let me at 'em!
-> DONE

= no_fight
Can't we all just get along?
-> DONE
