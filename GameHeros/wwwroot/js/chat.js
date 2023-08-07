"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

var gameObject;

connection.on("ReceiveMessage", async function (message, ip) {

    var li = document.createElement("li");
    document.getElementById("messagesList").appendChild(li);
    li.textContent = ` says ${message} ${ip}`;
});

connection.on("PlayerTurnStart", function (jsonPage) {
    console.log(jsonPage);
    gameObject = JSON.parse(jsonPage);
    RenderPlayer(jsonPage);
})

connection.start().then(function () {
    
    //document.getElementById("sendButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

function RenderPlayer(dados) {

    dados = JSON.parse(dados);

    var jogadores = dados.PlayerList;

    var divPlayer1 = document.getElementById("player1");
    var divPlayer2 = document.getElementById("player2");

    RenderPlayerContainer(divPlayer1, jogadores[0]);
    //RenderPlayerContainer(divPlayer2, jogadores[1]);
}

function CreateStatusElement(name, information) {
    var p = document.createElement("p");
    var bName = document.createElement("b");
    bName.textContent = name;

    p.appendChild(bName);
    var valueP = ": " + information;

    p.textContent = bName.textContent + valueP;

    return p;
}
function RenderPlayerContainer(container, jogador) {

    var img = document.createElement("img");
    img.src = jogador.Hero.HeroImage;
    img.className = "img-hero";

    var name = document.createElement("h3");
    name.textContent = jogador.Hero.Name;

    var health  = jogador.Hero.HealthPoints;
    var attack  = jogador.Hero.AttackPoints;
    var defense = jogador.Hero.Defense;

    var dHealth   = CreateStatusElement("Health", health);
    var dAttack   = CreateStatusElement("Attack", attack);
    var dDefense  = CreateStatusElement("Defense", defense);

    container.appendChild(name);
    container.appendChild(dHealth);
    container.appendChild(dAttack);
    container.appendChild(dDefense);
    container.appendChild(img);

    var divSkills = document.createElement("div");
    divSkills.className = "div-skills";

    var skills = jogador.Hero.HeroSkills;

    AddListSkills(divSkills, skills);

    container.appendChild(divSkills);
}

function AddListSkills(container, listSkills) {

    for (var i = 0; i < listSkills.length; i++) {
        var skill = listSkills[i];

        var skillRender = AddSkillReturn(skill);
        container.appendChild(skillRender);
    }
}

function AddSkillReturn(skill) {

    var divSkill = document.createElement("div");
    var img = document.createElement("img");
    divSkill.className = "div-skill";

    var divDescriptionSKill = document.createElement("div");
    divDescriptionSKill.className = "div-hidden";
    
    for (const status in skill) {

        if (status == "Id") {
            var idSkill = document.createElement("hidden");
            idSkill.value = skill[status];
            divDescriptionSKill.appendChild(idSkill);
        }

        if (skill[status] != undefined && skill[status] != 0 && status != "Image" && status != "Id") {
            var description = status;
            var value = skill[status];
            var p = document.createElement("p");

            p.textContent = description + ": " + value;
            divDescriptionSKill.appendChild(p);
        }

        if (status == "Image") {
            img.src = skill[status];
        }
    }
    
    divSkill.appendChild(img);
    divSkill.appendChild(divDescriptionSKill);

    img.addEventListener("mouseover", (e) => {
        var divSkill = e.target.parentElement;

        var divDescription = divSkill.getElementsByTagName("div");

        divDescription.className = "div-show";
        divDescriptionSKill.className = "div-show";
        console.log(divDescription.className);
    });

    img.addEventListener("mouseout", (e) => {
        var divSkill = e.target.parentElement;

        var divDescription = divSkill.getElementsByTagName("div");
        
        divDescription.className = "div-hidden";
        divDescriptionSKill.className = "div-hidden";
        console.log(divDescription.className);
    });

    img.addEventListener("click", activateSkill);
    return divSkill;
}

function activateSkill(imageSkill) {
    var skillId = imageSkill.target.parentElement.children[1].getElementsByTagName("hidden")[0].value
    gameObject.CurrentSkillId = skillId;
    connection.invoke("MakeTurn", gameObject).catch(function (err) {
        return console.error(err.toString());
    });
}

function StartGame() {
    connection.invoke("ConnectPlayer").catch(function (err) {
        return console.error(err.toString());
    });
}
