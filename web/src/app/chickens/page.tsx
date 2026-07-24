import React from 'react'

const page = () => {
    return(
        <main className="min-h-screen flex items-center justify-center pt-18">
            <div className="flex justify-center gap-8 w-[90%] p-5">
                <div className="w-[35%] rounded-2xl border-4 border-olive-900 bg-olive-700 p-1">
                    <img src="/burger.gif"
                    alt="guy eating burger"
                    className="w-full h-auto rounded-xl"
                    />
                </div>
                <div className="w-60 rounded-2xl flex flex-col items-center justify-evenly border-4 border-olive-900 bg-olive-700">
                    <div className=""></div>
                </div>
            </div>
        </main>
    )
}


export default page