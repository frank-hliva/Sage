module Object (
    
    receive = func messages -> (
        messages
    )

    dispatch = func (msg) -> (

    )

    sendMessage = (msg) (obj) => (
        let msg = receive msg
        self.dispatch msg

    )
)