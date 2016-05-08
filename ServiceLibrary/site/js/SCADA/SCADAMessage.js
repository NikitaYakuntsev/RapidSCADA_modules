/**
 * Created by Никита on 01.02.2016.
 */

function test(a,b) {
    return a+b;
}

function MessageClient(id) {
    var SCADAIP;
    var serverKey;
    var myKeys;
    var escalate = 0;
    var succeed = true;
    //TODO: get SCADA IP from cloud server
    SCADAIP = "http://83.139.178.57:8000/";
    localforage.setDriver(localforage.INDEXEDDB);
    localforage.getItem(id,function(err, value) {
        if (value == null) {
            succeed = false;
            // alert("you haven't authorized to this SCADA");
        }
        if (!succeed)
            return;
        //TODO: prepare a CryptoKey in serverKey
    });
    localforage.getItem("keys", function(err, value) {
        if (value == null) {
            succeed = false;
            //alert("you haven't generated your keys");
        }
        if (!succeed)
            return;
        //TODO: decrypt private key
        //TODO: create CryptoKeyPair and place to myKeys
    });
    /**
     * Sends a message to a server and returns
     * message - string, containing a message to a server
     * callback - function to get the answer
     */
    this.sendPostMessage = function (path, message, callback) {
        //TODO: cypher with CryptoKey
        //TODO: sign with CryptoKeyPair's private
        $.ajax({
            type:"POST",
            url:SCADAIP + path,
            data:message,
            success: function(data, textStatus, jqXHR) {
                callback(new MessageResponse(textStatus, data));
            },
            contentType:"application/json; charset=utf-8",
            dataType: "json"
        });
    }

    this.sendGetMessage = function (path, message, callback) {
        //TODO: cypher with CryptoKey
        //TODO: sign with CryptoKeyPair's private
        $.ajax({
            type:"POST",
            url:SCADAIP + path,
            success: function(data, textStatus, jqXHR) {
                callback(new MessageResponse(textStatus, data));
            },
            contentType:"application/json; charset=utf-8",
            dataType: "json"
        });
    }
    return this;
}

function MessageResponse(code, msg) {
    this.code = code;
    this.message = msg;
}