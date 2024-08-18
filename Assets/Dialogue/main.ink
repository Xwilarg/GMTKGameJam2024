/* 
GMTK Game Jam 2024
Theme: Built to Scale
Gameplay: Build robots based on customer commissions. Scale up robots and expand business.
Writing needs: Dialogue
*/

// Customers
VAR bot_number = 0

-> mayor.game_intro
== mayor ==
{ 
  - not guyd_bot.tutorial: -> game_intro
}
= game_intro
~ bot_number = 1
Congratulations on your new shop! As mayor, I'm always eager to support small businesses in this town! # speaker mayor
Speaking of support, I need your help. # speaker mayor
City Hall is a stinking mess today - turns out a nest of rats has been living in our vents!  Could you send {bot_number} of your cats to take care of the situation? # speaker mayor
-> guyd_bot.tutorial

= host_event
~ bot_number = RANDOM(1, 20)
Hello, your shop seems to be doing well! My office pushed for those tax incentives in the last election - we're big on helping businesses! # speaker mayor
Anyway, we're hosting a delegation from our sister city this week and our regular caterers are overbooked. Do you have a team of {bot_number} that can prepare {~Greek food|brown bag lunches|pizzas|BBQ ribs|sandwiches}?
-> DONE

== fire_chief ==
= forest_fire
~ bot_number = RANDOM(1, 20)
The forest fire is out of control and heading towards town. My people need all the help they can get. Give me {bot_number} of your best firefighters! # speaker fire_chief
-> DONE

== mafia ==
= turf_war
~ bot_number = RANDOM(1, 20)
New business eh, bro? You should meet my boss Don Fusilli. You'll need his help one day. # speaker mafia
Don Fusilli takes care of his own. Question is, will you step up for the Don? The {~Ziti|Rigatoni|Gemelli|Farfalle|Cavatappi} are trying to steal our territory and Don Fusilli needs {bot_number} fighters. # speaker mafia
-> DONE

== rancher ==
= lost
~ bot_number = RANDOM(1, 20)
S'up amigo? Some of our cattle escaped and I sure could use some help tracking 'em down. You got {bot_number} cowboys 'round these parts? # speaker rancher
-> DONE

// Bots
VAR build_success = true
VAR found_job = 1
LIST bot_tasks = cooking, cowboy, kitty, firefighting, battling, repair
VAR task = repair

== guyd_bot ==
= tutorial
New day, new beginnings, I say! Let me show you around! # speaker guyd 
Fear not, I was Best Teaching Assistant five years in a row. You're in good hands! # speaker guyd
// What's in the tutorial?
-> DONE

= scan_jobs
Hmm, wonder what's going on in town? # speaker guyd
~ found_job = RANDOM(0, 1)
{
  - found_job == 1: -> new_task
  - else: -> no_task
}

= new_task
~ task = LIST_RANDOM(bot_tasks)
{
  - task == "cowboy": -> cowboy_task
  - task == "kitty": -> kitty_task
  - else: -> report_task
}

= cowboy_task
~ bot_number = RANDOM(1, 20)
Hey, let's send {bot_number} {bot_number>1:cowboys|cowboy} to the ranch. Looks like their cattle got loose again. # speaker guyd
-> DONE

= kitty_task
~ bot_number = RANDOM(1, 20)
Snakes alive! We need {bot_number} {bot_number>1:cats|cat} at that house before anyone gets attacked. # speaker guyd
-> DONE

= report_task
Oh, someone needs a {task} job done! Let's get on it, {bot_number} {bot_number>1:bots|bot} coming right up! # speaker guyd
-> DONE

= no_task
Quiet day, I guess. Slow day for business too... # speaker guyd
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
  - build_success: -> can_cook
  - else: -> cannot_cook
}

= can_cook
I can make you {~baklava|lasagna|ice cream|chicken cordon bleu|sushi}!
-> DONE

= cannot_cook
Help! How do you boil water?
-> DONE

== cowboy_bot ==
{
  - build_success: -> rodeo
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
  - build_success: -> is_cat
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
  - build_success: -> hot
  - else: -> not_hot
}

= hot
There's no smoke without... FIRE! I'll take care of this!
-> DONE

= not_hot
Why am I smoking?! And I don't mean hot.
-> DONE

== battle_bot ==
{
  - build_success: -> fight
  - else: -> no_fight
}

= fight
Let me at 'em!
-> DONE

= no_fight
Can't we all just get along?
-> DONE

== repair_bot ==
{
  - build_success: -> fix
  - else: -> broken
}

= fix
I'll grab my {~hammer|wrench|screwdriver}. This'll take no time at all!
-> DONE

= broken
I might have a screw or two loose, but who doesn't, right am I?
-> DONE
