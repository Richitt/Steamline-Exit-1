using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunPrinter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	public static void PrintPun (int num) {
        //accepts the corresponding number for the object dropped and DisplayDialogues a pun
        switch (num)
        {
            case 1:
                UIBehavior.DisplayDialogue("I'll reach the top in no time! I've gotta keep this can-do attitude!");
                break;
            case 2:
                UIBehavior.DisplayDialogue("Lemme just turn this table to fit better.");
                break;
            case 3:
                UIBehavior.DisplayDialogue("It's bin nice using this to build my tower.");
                break;
            case 4:
                UIBehavior.DisplayDialogue("Oh! This trash really takes the cake!");
                break;
            case 5:
                UIBehavior.DisplayDialogue("This is the perfect piece to get me out of this jam!");
                break;
            case 6:
                UIBehavior.DisplayDialogue("I find this piece of trash the most a-peel-ing!");
                break;
            case 7:
                UIBehavior.DisplayDialogue("I'd make acorn-y pun but you wouldn't want to hear it.");
                break;
            case 8:
                UIBehavior.DisplayDialogue("What is this? How are you supposed to stack these things?");
                break;
            case 9:
                UIBehavior.DisplayDialogue("Oh... I guess anything helps but that's so small.");
                break;
            case 10:
                UIBehavior.DisplayDialogue("It's a-boat time I got myself one of these!");
                break;
            case 11:
                UIBehavior.DisplayDialogue("Who's birthday is it today? I guess I'll just keep this to myself!");
                break;
            case 12:
                UIBehavior.DisplayDialogue("A stuffed puppy! I'm a bit of a retriever myself!");
                break;
            case 13:
                UIBehavior.DisplayDialogue("I soap there's something good at the end of this.");
                break;
            case 14:
                UIBehavior.DisplayDialogue("A fatherly figureine!");
                break;
            case 15:
                UIBehavior.DisplayDialogue("It condo looks like someone's home.");
                break;
            case 16:
                UIBehavior.DisplayDialogue("I don't know what this is. It's tough to behold.");
                break;
            case 17:
                UIBehavior.DisplayDialogue("Looks like something you'd find in a landfill.");
                break;
            case 18:
                UIBehavior.DisplayDialogue("The future is looking bright!");
                break;
            case 19:
                UIBehavior.DisplayDialogue("A coat almost as beautiful as mine.");
                break;
            case 20:
                UIBehavior.DisplayDialogue("I've gotta give credit to the folks who threw this out!");
                break;
            case 21:
                UIBehavior.DisplayDialogue("I almost brushed past this useful item.");
                break;
            case 22:
                UIBehavior.DisplayDialogue("Be sure not to stack four items of the same color, otherwise we'll take a plunge.");
                break;
            case 23:
                UIBehavior.DisplayDialogue("Bear with me, I think we could use this!");
                break;
            case 24:
                UIBehavior.DisplayDialogue("I think we're almost pasta point of no return.");
                break;
            case 25:
                UIBehavior.DisplayDialogue("Your building methods are kinda sketch.");
                break;
            case 26:
                UIBehavior.DisplayDialogue("This piece of junk tickles my fancy.");
                break;
            case 27:
                UIBehavior.DisplayDialogue("Looks like something's wrrong with this cat.");
                break;
            case 28:
                UIBehavior.DisplayDialogue("This really lights up the place!");
                break;
            case 29:
                UIBehavior.DisplayDialogue("A game? Why would someone throw this away?");
                break;
            case 30:
                UIBehavior.DisplayDialogue("What a rice little piece to add to my collection");
                break;
            case 31:
                UIBehavior.DisplayDialogue("Is it a bit cheesy to hoard these?");
                break;
            case 32:
                UIBehavior.DisplayDialogue("Is it a unicorn horn?");
                break;
            case 33:
                UIBehavior.DisplayDialogue("My hat's off to ya for making it this far!");
                break;
            case 34:
                UIBehavior.DisplayDialogue("This box doesn't look very stable.");
                break;
            case 35:
                UIBehavior.DisplayDialogue("What are these things?");
                break;
            case 36:
                UIBehavior.DisplayDialogue("What a strange looking set of blocks.");
                break;
            case 37:
                UIBehavior.DisplayDialogue("A pony! It sounds a little horse.");
                break;
            case 38:
                UIBehavior.DisplayDialogue("There ain't muffin holding us back now!");
                break;
            case 39:
                UIBehavior.DisplayDialogue("You don't have to hammer it in that we're taking a while to get out of here!");
                break;
            case 40:
                UIBehavior.DisplayDialogue("A cat octopus? Meow I've seen it all!");
                break;
            case 41:
                UIBehavior.DisplayDialogue("I don't know rye someone would throw out perfectly good bread.");
                break;
            case 42:
                UIBehavior.DisplayDialogue("Just keep stacking and leave the chest to me!");
                break;
            case 43:
                UIBehavior.DisplayDialogue("When did disco out of fashion?");
                break;
            case 44:
                UIBehavior.DisplayDialogue("Seems like something suspicious is stewing in there!");
                break;
            case 45:
                UIBehavior.DisplayDialogue("I've got the core essentials of this tower set!");
                break;
            case 46:
                UIBehavior.DisplayDialogue("I've cracked the code of this challenge!");
                break;
            case 47:
                UIBehavior.DisplayDialogue("And they said using lightbulbs wasn't a bright idea, hah!");
                break;
            case 48:
                UIBehavior.DisplayDialogue("I've grown to chair about all this trash.");
                break;
            case 49:
                UIBehavior.DisplayDialogue("I just need a way to snake out of this alley!");
                break;
            case 50:
                UIBehavior.DisplayDialogue("This cloud doesn't look very creative.");
                break;
            default:
                UIBehavior.DisplayDialogue("Look at my collection!");
                break;
        }


    }
}
