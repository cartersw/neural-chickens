import React from 'react'

const page = () => {
    return(
        <main className="min-h-screen flex items-center justify-center pt-24">
            <div className="flex gap-8 p-5">
                <div className="rounded-2xl border border-gray-700 bg-gray-900 p-3">
                    <img src="/burger.gif"
                    alt="guy eating burger"
                    className="w-full h-auto rounded-xl"
                    />
                </div>
                <div className="w-40 bg-gray-900 rounded-xl">
                    <div className=""></div>
                    

                </div>
            </div>
        </main>
    )
}


export default page