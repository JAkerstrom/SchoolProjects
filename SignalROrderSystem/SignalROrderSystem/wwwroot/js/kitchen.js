"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/restauranthub").build();

connection.on("NewOrder", function (name, phonenumber, orderid, itemname) {
    var div = document.createElement('div');
    div.className = 'panel panel-default';
    div.id = orderid;
    div.innerHTML = '<div class="panel-body">'+
        '<div class="pull-left col-sm-4">' +
        '<div class="com-sm-12"><p>Namn:' + name + '</p></div>' +
        '<div class="com-sm-12"><p>Tel:' + phonenumber + '</p></div>' +
        '</div>'+
        '<div class="pull-right col-sm-8">' +
        '<div class="pull-left col-sm-12 col-sm-6">' +
        '<h3>' + itemname + '</h3>' +
        '</div>' +
        '<div class="pull-right col-sm-12 col-md-6">' +
        '<form class="btn-group" role="group">' +
        '<input type="submit" value="Påbörjad" id="begin+' + orderid + '" class="btn btn-default" />' +
        '<input type="submit" value="Klar" id="finnish+' + orderid + '" class="btn btn-default" />' +
        '<input type="submit" value="Hämtad" id="pickup+' + orderid + '" class="btn btn-default" />' +
            '</form> </div>'+
        '</div>'+
        '</div>';
    document.getElementById("orderlist").appendChild(div);

    document.getElementById('begin+' + orderid).addEventListener("click", function (event) {
        connection.invoke("BeginOrder", orderid.toString()).catch(function (err) {
            return console.error(err.toString());
        });
        event.preventDefault();
    });

    document.getElementById('finnish+' + orderid).addEventListener("click", function (event) {
        connection.invoke("FinnishOrder", orderid).catch(function (err) {
            return console.error(err.toString());
        });
        event.preventDefault();
    });

    document.getElementById('pickup+' + orderid).addEventListener("click", function (event) {
        document.getElementById(orderid).remove();
        connection.invoke("PickupOrder", orderid).catch(function (err) {
            return console.error(err.toString());
        });
        event.preventDefault();
    });
});


connection.start().catch(function (err) {
    return console.error(err.toString());
});

(function () {
    var forms = document.getElementsByClassName("jsformdiv");
    
    var i;
    for (i = 0; i < forms.length; i++) {
        var div = forms[i];
        var orderid = div.id;
        var children = div.children[0];

        var i;
        for (i = 0; i < children.length; i++) {
            if (i === 0) {
                children[i].addEventListener("click", function (event) {
                    connection.invoke("BeginOrder", orderid).catch(function (err) {
                        return console.error(err.toString());
                    });
                    event.preventDefault();
                });
            } else if (i === 1) {
                children[i].addEventListener("click", function (event) {
                    connection.invoke("FinnishOrder", orderid).catch(function (err) {
                        return console.error(err.toString());
                    });
                    event.preventDefault();
                });
            } else if (i === 2) {
                children[i].addEventListener("click", function (event) {

                    document.getElementById("parentDiv").remove();
                    connection.invoke("PickupOrder", orderid).catch(function (err) {
                        return console.error(err.toString());
                    });
                    event.preventDefault();
                });
            }            
        }
    }
})();





