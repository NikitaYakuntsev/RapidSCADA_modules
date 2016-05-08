/**
 * Created by Никита on 07.02.2016.
 */

function getSCADAList(callback) {
    localforage.setDriver(localforage.INDEXEDDB);
    localforage.getItem("SCADAList",function(err, value) {
        if (value == null) {
            callback("You haven't authorized to any SCADA");
            return;
        }
        var result = [];
        value.forEach(function(item, i, arr) {
            result.push(new SCADA(item.id, item.name));
        });
        callback(result);
    });
}

function SCADA(id, name) {
    this.id = id;
    this.name = name;
    this.address = "";

    this.setIP = function (ip) {
        this.id = ip;
    }

    return this;
}